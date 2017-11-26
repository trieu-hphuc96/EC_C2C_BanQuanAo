using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EC_C2C_BanQuanAo.Startup))]
namespace EC_C2C_BanQuanAo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
