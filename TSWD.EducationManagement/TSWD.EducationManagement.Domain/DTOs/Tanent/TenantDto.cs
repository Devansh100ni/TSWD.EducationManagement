namespace TSWD.EducationManagement.Domain.DTOs.Tanent
{
    public class TenantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int NoOfUsers { get; set; }
    }
}
