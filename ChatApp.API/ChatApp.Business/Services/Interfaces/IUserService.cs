﻿using ChatApp.Business.DTO_s.Autheticate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task Register(Register register);
    }
}
