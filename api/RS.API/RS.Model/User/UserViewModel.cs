﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RS.ViewModel.User
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
