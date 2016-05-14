using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Net51Zhang.Common;
using Net51Zhang.DBRepository;
using Net51Zhang.Models;
using Net51Zhang.Service;
using Net51Zhang.Common.Cache;
using Net51Zhang.Models.DataModel;

namespace Net51Zhang.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IServiceManager serviceManager) : base(serviceManager)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
