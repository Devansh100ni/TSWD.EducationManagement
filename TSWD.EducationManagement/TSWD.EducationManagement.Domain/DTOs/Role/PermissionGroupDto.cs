namespace TSWD.EducationManagement.Domain.DTOs.Role
{
    public class PermissionGroupDto
    {
        public string GroupName { get; set; } = string.Empty;

        public List<PermissionItemDto> Permissions { get; set; } = new();
    }
}
