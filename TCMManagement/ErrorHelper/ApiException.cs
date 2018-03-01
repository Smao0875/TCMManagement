using System;
using System.Net;
using System.Runtime.Serialization;

/// <summary>
/// BORROWED FROM :
/// https://www.codeproject.com/Articles/1028416/RESTful-Day-sharp-Request-logging-and-Exception-ha
/// </summary>
namespace TCMManagement.ErrorHelper
{
    /// <summary>
    /// Api Exception
    /// </summary>
    [Serializable]
    public class ApiException : Exception, IApiExceptions
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string ErrorDescription { get; set; }
        [DataMember]
        public HttpStatusCode HttpStatus { get; set; }

        string reasonPhrase = "ApiException";

        [DataMember]
        public string ReasonPhrase
        {
            get { return this.reasonPhrase; }

            set { this.reasonPhrase = value; }
        }
    }
}