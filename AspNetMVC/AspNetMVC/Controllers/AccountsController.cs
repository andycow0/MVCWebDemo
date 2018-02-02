using AspNetMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace AspNetMVC.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [AllowAnonymous]
        [HandleError]
        [OutputCache(Duration = 3600)] //, VaryByParam = "none", Location = OutputCacheLocation.Client, NoStore = true)]
        public ActionResult Login()
        {
            ViewBag.Time2 = DateTime.Now.ToLongTimeString();

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginMyViewModel loginUser, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (LoginResult(loginUser))
                {
                    FormsAuthentication.SetAuthCookie(loginUser.UserName, loginUser.RememberMe);
                    return RedirectToAction("Index", "Customers");
                }
                string err = TempData["LoginErrMsg"].ToString() + "。";
                ModelState.AddModelError("", TempData["LoginErrMsg"].ToString());
            }

            return View(loginUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();

            //// Outputcache root
            var url = Url.Action("Login", "Accounts");
            //// Clean output cache by root
            HttpResponse.RemoveOutputCacheItem(url);

            return RedirectToAction("Login", "Accounts");   // Index, Home
        }

        private bool LoginResult(LoginMyViewModel loginUser)
        {
            string strJson = string.Empty;

            //using (WebService1SoapClient webService = new WebService1SoapClient())
            //{
            //    strJson = webService.Get_Authority(loginUser.UserName, loginUser.Password, "51", "10");
            //    webService.Close();
            //}
            //JObject jObject = JObject.Parse(strJson);

            //bool result = jObject["isSuccess"].ToString() == "Y" ? true : false;

            //if (result)
            //{
            var returnCode = string.Equals(loginUser.UserName.ToUpper(), "TEST") ?
            0 : 1;    //jObject[""][0]["ReturnCode"].ToString();

            if (returnCode.Equals(0))
            {
                //string userName = jObject[""][0]["doctorName"].ToString();
                var userName = "Test1234";

                FormsAuthentication.RedirectFromLoginPage(loginUser.UserName, false);

                var authTicket = new FormsAuthenticationTicket(
                1,
                loginUser.UserName,     //  你想要存放在 User.Identy.Name 的值，通常是使用者帳號
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                loginUser.RememberMe,   //  將管理者登入的 Cookie 設定成 Session Cookie
                userName,               //  userdata看你想存放啥
                FormsAuthentication.FormsCookiePath);

                // Encrypt the ticket.
                string encTicket = FormsAuthentication.Encrypt(authTicket);

                // Create the cookie.
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                return true;    //"登入成功";
            }
            else if (returnCode.Equals(1))
            {
                TempData["LoginErrMsg"] = "帳號或密碼不正確";
                return false;
            }
            else if (returnCode.Equals(7))
            {
                TempData["LoginErrMsg"] = "該帳號不存在";
                return false;
            }
            //}
            // Default error.
            TempData["LoginErrMsg"] = "帳號或資料傳輸錯誤";
            return false;
        }
    }
}