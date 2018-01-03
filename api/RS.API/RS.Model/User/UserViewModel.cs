﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RS.ViewModel.User
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }
    }
}
