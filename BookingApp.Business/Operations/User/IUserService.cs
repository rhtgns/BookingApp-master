﻿using BookingApp.Business.Operations.User.Dtos;
using BookingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user); // async çünkü unitofwork kullanılacak
        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);
    }
}
