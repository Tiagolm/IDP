﻿using Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Contact
{
    internal class AdminContactResponse : ContactResponse
    {
        public UserResponse User { get; set; }
    }
}
