using System.Web.Mvc;

namespace EC_C2C_BanQuanAo.Areas.NguoiBan
{
    public class NguoiBanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NguoiBan";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NguoiBan_default",
                "NguoiBan/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}