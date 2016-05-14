using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Net51Zhang.Common;
using Net51Zhang.Models.Manager;

namespace Net51Zhang.Controllers
{
    [AdminAuthorize]
    public class ManagerController : BaseController
    {
        public ManagerController(IServiceManager service) : base(service)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var exists =
                    this.ServiceManager.AccountService.GetAll()
                        .FirstOrDefault(entity => entity.Name.Equals(model.Name, StringComparison.Ordinal));
                if (exists != null)
                {
                    ModelState.AddModelError("", model.Name + " 已经存在");
                }
                else
                {
                    if (!model.Password.Equals(model.ConfirmPassword, StringComparison.Ordinal))
                    {
                        ModelState.AddModelError("", "密码不一致");
                    }
                    else
                    {
                        this.ServiceManager.AccountService.Insert(new AccountEntity()
                        {
                            Name = model.Name,
                            HashedPassword = Utils.GetMd5Hash(model.Password),
                            CreateTime = DateTime.Now,
                            ModifyTime = DateTime.Now
                        });

                        return this.RedirectToAction("Login", "Manager");
                    }
                }
            }

            return this.View();
        }

        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                AccountEntity account = this.ServiceManager.AccountService.GetAll()
                    .FirstOrDefault(entity => entity.Name.Equals(model.Name, StringComparison.Ordinal));
                if (account == null)
                {
                    ModelState.AddModelError("", "不存在该用户");
                }
                else if(!account.HashedPassword.Equals(Utils.GetMd5Hash(model.Password)))
                {
                    ModelState.AddModelError("", "密码错误");
                }
                else
                {
                    var encryptionProvider = this.ServiceManager.EncryptionProvider;
                    var nameCook = new HttpCookie(Constant.AccountCookieUserKeyHashed, encryptionProvider.Encrypt(account.Name));
                    var valueCook = new HttpCookie(Constant.AccountCookiePsdKeyHashed, encryptionProvider.Encrypt(account.HashedPassword));
                    nameCook.Expires = DateTime.Now.AddHours(1);
                    valueCook.Expires = DateTime.Now.AddHours(1);

                    this.Response.Cookies.Add(nameCook);
                    this.Response.Cookies.Add(valueCook);

                    return this.RedirectToAction("Index", "Manager");
                }
            }

            return this.View();
        }
    }
}