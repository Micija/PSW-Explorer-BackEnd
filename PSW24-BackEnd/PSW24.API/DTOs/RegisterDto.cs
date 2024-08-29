using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW24.API.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }    
        public string Surname { get; set; }
        public UserRoleDto Role { get; set; }
        public List<string> Interests { get; set; }
    }
}
