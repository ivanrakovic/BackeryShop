using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BackeryShop.Web.Startup))]
namespace BackeryShop.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
