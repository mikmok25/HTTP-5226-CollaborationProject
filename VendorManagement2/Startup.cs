using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VendorManagement2.Startup))]
namespace VendorManagement2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
