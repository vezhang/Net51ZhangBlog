using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Net51Zhang.Common;
using Net51Zhang.Models.Error;

namespace Net51Zhang.Controllers
{
    public class ErrorController : BaseController
    {
        public ErrorController(IServiceManager service) : base(service)
        {
        }

        public ActionResult PageNotFound()
        {
            return this.View();
        }

        public ActionResult Exception(ExceptionModel model)
        {
            return this.View("Exception", model);
        }
    }
}