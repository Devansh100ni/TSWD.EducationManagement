namespace TSWD.EducationManagement.Domain.Entities
{
    public class AppApiRequestLog
    {
        public int Id { get; set; }
        public string? Path { get; set; }
        public string? Method { get; set; }
        public string? QueryString { get; set; }
        public string? Headers { get; set; }
        public string? Body { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? InnerException { get; set; }
        public string? StackTrace { get; set; }
        public long DurationMs { get; set; }

        public Guid? UserId { get; set; } = null;
        public Guid? TenantId { get; set; } = null;
    }
}
