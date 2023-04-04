using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lubnamaster.Startup))]
namespace lubnamaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
