using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SbdS.Startup))]
namespace SbdS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
