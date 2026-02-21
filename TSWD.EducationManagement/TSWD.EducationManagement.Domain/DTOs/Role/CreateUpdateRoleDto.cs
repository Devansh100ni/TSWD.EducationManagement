namespace TSWD.EducationManagement.Domain.DTOs.Role
{
    public class CreateUpdateRoleDto
    {
        public CreateUpdateRoleDto()
        {
            Permissions = new List<AddPermissionDto>();
        }
        public Guid? Id { get; set; }
        public Guid? TenantId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<AddPermissionDto> Permissions { get; set; }
    }
}
