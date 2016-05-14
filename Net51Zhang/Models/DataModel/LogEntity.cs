using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Net51Zhang.Models.DataModel
{
    public class LogEntity: BaseEntity
    {
        public DateTime CreateTime { get; set; }
        public string Message { get; set; }
    }
}