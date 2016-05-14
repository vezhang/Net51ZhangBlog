using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Net51Zhang.Common;
using Net51Zhang.Common.TaskWrapper;
using Net51Zhang.Controllers;
using Net51Zhang.DBRepository;
using Net51Zhang.Models;
using Net51Zhang.Models.DataModel;
using Net51Zhang.Service;

namespace Net51Zhang
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ContainerManager.RegisterComponent();

            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();
        }

        protected void Application_Error()
        {
            var httpException = this.Server.GetLastError() as HttpException;
            if (httpException != null && (httpException.GetHttpCode() == 404 || httpException.GetHttpCode() == 403))
            {
                this.Response.Clear();
                this.Server.ClearError();

                var routeData = new RouteData();
                routeData.Values["controller"] = "Error";
                routeData.Values["action"] = "PageNotFound";
                IController errorController = new ErrorController(null);
                var rc = new RequestContext(new HttpContextWrapper(this.Context), routeData);
                errorController.Execute(rc);
            }
        }
    }
}