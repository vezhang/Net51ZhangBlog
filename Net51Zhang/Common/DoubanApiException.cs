using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Net51Zhang.Common
{
    public class DoubanApiException : Exception
    {
        public DoubanApiException(string message) : base(message)
        {}
    }
}