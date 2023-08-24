using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TekhneCafe.Core.DTOs.Authentication
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public float Wallet { get; set; }
        public string Role { get; set; }
    }
}
