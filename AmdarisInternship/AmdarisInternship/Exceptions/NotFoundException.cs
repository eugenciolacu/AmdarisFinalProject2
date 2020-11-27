using System.Net;

namespace AmdarisInternship.API.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {

        }
    }
}
