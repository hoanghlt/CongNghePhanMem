using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCVSMobile.Startup))]
namespace MVCVSMobile
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
