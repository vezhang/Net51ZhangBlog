using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Net51Zhang.Models.Error
{
    public class ExceptionModel
    {
        public string IpAddress { get; set; }

        public string Browser { get; set; }

        public string BrowserVersion { get; set; }

        public string Url { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return string.Format("Url:{0}, IpAddress:{1}, Browser:{2}-{3}, Message:{4}.", this.Url, this.IpAddress,
                this.Browser, this.BrowserVersion, this.Message);
        }
    }
}