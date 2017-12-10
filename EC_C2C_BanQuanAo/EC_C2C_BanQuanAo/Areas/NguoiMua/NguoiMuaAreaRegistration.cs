using System.Web.Mvc;

namespace EC_C2C_BanQuanAo.Areas.NguoiMua
{
    public class NguoiMuaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NguoiMua";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "NguoiMua_default",
                "NguoiMua/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}