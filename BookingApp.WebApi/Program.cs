using BookingApp.Business.DataProtection;
using BookingApp.Business.Operations.Feature;
using BookingApp.Business.Operations.Hotel;
using BookingApp.Business.Operations.Setting;
using BookingApp.Business.Operations.User;
using BookingApp.Data.Context;
using BookingApp.Data.Repositories;
using BookingApp.Data.UnitOfWork;
using BookingApp.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Jwt Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer Token On Texbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }

    };
    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>() }
    });
});
builder.Services.AddScoped<IDataProtection, DataProtection>();
var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));
builder.Services.AddDataProtection()
                .SetApplicationName("BookingApp")
                .PersistKeysToFileSystem(keysDirectory);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidateLifetime = true, //süresi dolan tokeni kabul etme
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
                    };
                });
var connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<BookingAppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));//generic oldugu için typeof kullanýmý
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService,UserManager>();
builder.Services.AddScoped<IFeatureService,FeatureManager>();
builder.Services.AddScoped<IHotelService,HotelManager>();
builder.Services.AddScoped<ISettingService,SettingManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//maintenancemode
app.UseMaintenanceMode();

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
