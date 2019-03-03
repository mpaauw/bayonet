using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace bayonet.Core.Common
{
    public class Result<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public T Value { get; set; }

        public bool IsError { get; set; }

        public string ErrorMessage { get; set; }
    }
}
