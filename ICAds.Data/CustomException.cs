using System;
using System.IO;
using System.Net;

namespace ICAds.Data
{

    public abstract class CustomException : Exception
    {
    
        public abstract HttpStatusCode GetStatusCode();
        public bool Report { get; set; }
        public CustomException(string message, Exception e = null, bool reportError = true) : base(message, e)
        {
            Report = reportError;
        }

      
    }

    public class CustomGenericException : CustomException
    {
        public HttpStatusCode StatusCode { get; set; }

        public CustomGenericException(string message, HttpStatusCode code, bool reportError = true) : base(message, null, reportError)
        {
            StatusCode = code;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return StatusCode;
        }

    }

}