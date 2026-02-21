namespace TSWD.EducationManagement.Domain.DTOs.Role
{
    public class PermissionItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Display { get; set; } = string.Empty;
    }
}
