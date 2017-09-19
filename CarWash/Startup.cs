using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarWash.Startup))]
namespace CarWash
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
