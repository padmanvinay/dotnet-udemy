using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dto.User
{
    public class UserRegisterDto
    {
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = string.Empty;
    }
}