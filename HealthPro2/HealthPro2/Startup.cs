using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthPro2.Startup))]
namespace HealthPro2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
