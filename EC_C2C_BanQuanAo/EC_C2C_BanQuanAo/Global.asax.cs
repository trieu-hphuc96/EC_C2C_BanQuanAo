using EC_C2C_BanQuanAo.Models.Principal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace EC_C2C_BanQuanAo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie ck = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (ck != null)
            {
                FormsAuthenticationTicket fat = FormsAuthentication.Decrypt(ck.Value);
                TaiKhoan tk = JsonConvert.DeserializeObject<TaiKhoan>(fat.UserData);
                DangNhapPrincipal dnp = new DangNhapPrincipal(tk);
                HttpContext.Current.User = dnp;
            }
        }
    }
}
