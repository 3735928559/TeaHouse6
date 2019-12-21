using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeaHouse.Startup))]
namespace TeaHouse
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
