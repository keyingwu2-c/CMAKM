using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Simple02.Startup))]
namespace Simple02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
