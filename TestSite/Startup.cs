using Microsoft.Owin;
using Owin;
using TestSite;

[assembly: OwinStartup(typeof(Startup))]

namespace TestSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}