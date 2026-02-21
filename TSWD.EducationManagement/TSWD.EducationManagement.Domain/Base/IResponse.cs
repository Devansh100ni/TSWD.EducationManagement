using System.Net;

namespace TSWD.EducationManagement.Domain.Base
{
    public interface IResponse
    {
        public bool DidError { get; set; }

        public string ExceptionMessage { get; set; }

        public string ErrorMessage { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public TimeSpan TimeTaken { get; set; }
    }
}
