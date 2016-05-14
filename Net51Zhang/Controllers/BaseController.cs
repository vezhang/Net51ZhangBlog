using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Net51Zhang.Common;
using Net51Zhang.Models.DataModel;
using Net51Zhang.Models.Error;
using Net51Zhang.Service;

namespace Net51Zhang.Controllers
{
    public abstract class BaseController : Controller
    {
        private IServiceManager _serviceManager;
        public IServiceManager ServiceManager
        {
            get { return this._serviceManager; }
        }

        public BaseController(IServiceManager serviceManager)
        {
            this._serviceManager = serviceManager;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            var request = filterContext.RequestContext.HttpContext.Request;

            if (Utils.IsAjaxCall(request))
            {
                var code = HttpStatusCode.InternalServerError;
                if (filterContext.Exception is ArgumentException)
                {
                    code = HttpStatusCode.BadRequest;
                }

                filterContext.Result = new HttpStatusCodeResult(code, filterContext.Exception.ToString());
            }
            else
            {
                var unwrapperException = Utils.GetUnwrapperException(filterContext.Exception);
                string message = string.Format("{0}\r\n Stack Trace: {1}", unwrapperException.Message,unwrapperException.ToString());
                string url = request.RawUrl;
                string ip = Utils.GetRequestIPAddress(request);
                string browser = request.Browser.Browser;
                string version = request.Browser.Version;

                var viewResult = new ViewResult()
                {
                    ViewName = "~/Views/Error/Exception.cshtml",
                    ViewData = new ViewDataDictionary()
                };

                var model = new ExceptionModel()
                {
                    Browser = browser,
                    BrowserVersion = version,
                    IpAddress = ip,
                    Url = url,
                    Message = message
                };

                if (!Utils.IsLocalhost(request))
                {
                    this.ServiceManager.LogService.Insert(new LogEntity() {CreateTime = DateTime.Now, Message = model.ToString()});
                }

                viewResult.ViewData.Model = model;
                filterContext.Result = viewResult;
            }

            filterContext.ExceptionHandled = true;
        }
    }
}