using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kadam.DTO
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginValidateDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int ReporteeRoleId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
