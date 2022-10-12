using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Yacht.Startup))]
namespace Yacht
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
