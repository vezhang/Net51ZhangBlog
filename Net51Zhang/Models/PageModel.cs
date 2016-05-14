using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Net51Zhang.Common;

namespace Net51Zhang.Models
{
    public abstract class PageModel
    {
        private IServiceManager _serviceManager;
        public IServiceManager ServiceManager
        {
            get { return this._serviceManager; }
        }

        public PageModel(IServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
        }
    }

    public class AjaxResponse
    {
        public object Attach { get; set; }
        public object Data { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
    }
}