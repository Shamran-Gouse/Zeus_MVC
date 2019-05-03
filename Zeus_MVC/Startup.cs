using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Zeus_MVC.Startup))]
namespace Zeus_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
