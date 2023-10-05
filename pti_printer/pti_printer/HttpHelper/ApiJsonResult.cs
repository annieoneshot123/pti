using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiHelper
{
    public class ApiJsonResult<T>
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public string ErrorMessage { get; set; }
    }
}
