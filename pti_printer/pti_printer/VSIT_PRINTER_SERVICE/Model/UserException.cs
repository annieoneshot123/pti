﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VsitPrinter.Model
{
    public class UserException : Exception
    {
        public object Tag { get; set; }

        public UserException(string message) : base(message)
        {
        }

        public UserException(string message, object tag) : base(message)
        {
            Tag = tag;
        }

        public UserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UserException(string message, Exception innerException, object tag)
            : base(message, innerException)
        {
            Tag = tag;
        }
    }
}
