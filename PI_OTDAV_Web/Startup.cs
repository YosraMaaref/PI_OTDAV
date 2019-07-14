using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PI_OTDAV_Web.Startup))]
namespace PI_OTDAV_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
