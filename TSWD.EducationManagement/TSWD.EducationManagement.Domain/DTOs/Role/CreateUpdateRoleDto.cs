namespace TSWD.EducationManagement.Domain.DTOs.Role
{
    public class CreateUpdateRoleDto
    {
        public Guid? Id { get; set; }
        public Guid? TenantId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
