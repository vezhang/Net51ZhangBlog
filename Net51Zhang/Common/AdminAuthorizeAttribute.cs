using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Net51Zhang.Controllers;
using Net51Zhang.Models.Manager;

namespace Net51Zhang.Common
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.ActionDescriptor.ActionName == "Login" || filterContext.IsChildAction)
            {
                return;
            }
            else
            {
                var request = filterContext.HttpContext.Request;
                var nameCook = request.Cookies[Constant.AccountCookieUserKeyHashed];
                var valueCook = request.Cookies[Constant.AccountCookiePsdKeyHashed];
                
                BaseController controller = filterContext.Controller as BaseController;
                if (nameCook != null && valueCook != null && controller != null)
                {
                    var encryptionProvider = controller.ServiceManager.EncryptionProvider;
                    AccountEntity account = controller.ServiceManager.AccountService.GetAll()
                        .FirstOrDefault(
                            entity => entity.Name.Equals(encryptionProvider.Decrypt(nameCook.Value), StringComparison.Ordinal));

                    if (account != null && account.HashedPassword.Equals(encryptionProvider.Decrypt(valueCook.Value), StringComparison.Ordinal))
                    {
                        return;
                    }
                }

                filterContext.Result = new RedirectResult(controller.Url.Action("Login", "Manager"));
            }
        }
    }
}