using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

/// <summary>
/// BORROWED FROM :
/// https://www.codeproject.com/Articles/1028416/RESTful-Day-sharp-Request-logging-and-Exception-ha
/// </summary>
namespace TCMManagement.Helpers
{
    #region Status Object Class
    /// <summary>
    /// Public class to return input status
    /// </summary>
    [Serializable]
    [DataContract]
    public class ServiceStatus
    {
        #region Public properties.
        /// <summary>
        /// Get/Set property for accessing Status Code
        /// </summary>
        [JsonProperty("StatusCode")]
        [DataMember]
        public int StatusCode { get; set; }
        /// <summary>
        /// Get/Set property for accessing Status Message
        /// </summary>
        [JsonProperty("StatusMessage")]
        [DataMember]
        public string StatusMessage { get; set; }
        /// <summary>
        /// Get/Set property for accessing Status Message
        /// </summary>
        [JsonProperty("ReasonPhrase")]
        [DataMember]
        public string ReasonPhrase { get; set; }

        #endregion
    }

    #endregion
}