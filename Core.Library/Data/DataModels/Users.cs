﻿using System;
namespace Core.Library.Data.DataModels
{
    public class Users
    {
        public Users()
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
