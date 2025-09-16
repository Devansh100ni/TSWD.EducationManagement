namespace TSWD.EducationManagement.Domain.DTOs.Tanent
{
    public class CreateUpdateTenantDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
