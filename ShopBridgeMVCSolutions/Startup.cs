using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopBridgeMVCSolutions.Startup))]
namespace ShopBridgeMVCSolutions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
