using System.Net;

namespace HRA.Application.Common.Exceptions
{
    public class RestException : Exception
    {
        public RestException()
            : base()
        {
            Errors = new Dictionary<string, string[]>();
        }

        public RestException(HttpStatusCode code)
            : base("Hubo un error.")
        {
            Code = code;
            Errors = new Dictionary<string, string[]>();
        }

        public RestException(HttpStatusCode code, params string[] errors)
            : this()
        {
            Code = code;
            Errors = errors
                .ToDictionary(failureGroup => "Detalles", failureGroup => errors.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
        public HttpStatusCode Code { get; }
    }
}
