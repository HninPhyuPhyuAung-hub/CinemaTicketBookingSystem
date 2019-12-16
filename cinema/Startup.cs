using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cinema.Startup))]
namespace cinema
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
