using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSWD.EducationManagement.Domain.DTOs.Users
{
    public class UsersDtos
    {
        public Guid Id { get; set; }
        public Guid? RoleId { get; set; }
        public string? RoleName { get; set; }
        public string UserName { get; set; } = null!;
        public string? FullName { get; set; }
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
