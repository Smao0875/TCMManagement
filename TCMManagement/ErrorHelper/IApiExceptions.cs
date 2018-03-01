using System.Net;

/// <summary>
/// BORROWED FROM :
/// https://www.codeproject.com/Articles/1028416/RESTful-Day-sharp-Request-logging-and-Exception-ha
/// </summary>
namespace TCMManagement.ErrorHelper
{
    /// <summary>
    /// IApiExceptions Interface
    /// </summary>
    public interface IApiExceptions
    {
        /// <summary>
        /// ErrorCode
        /// </summary>
        int ErrorCode { get; set; }
        /// <summary>
        /// ErrorDescription
        /// </summary>
        string ErrorDescription { get; set; }
        /// <summary>
        /// HttpStatus
        /// </summary>
        HttpStatusCode HttpStatus { get; set; }
        /// <summary>
        /// ReasonPhrase
        /// </summary>
        string ReasonPhrase { get; set; }
    }
}
