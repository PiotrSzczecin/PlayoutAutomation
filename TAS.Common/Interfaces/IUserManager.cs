﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Common.Interfaces
{
    public interface IUserManager
    {
        IUser AddUser(string userName);

    }
}
