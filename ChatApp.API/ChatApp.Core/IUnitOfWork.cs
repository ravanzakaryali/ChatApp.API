﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
