using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Speakster.Startup))]
namespace Speakster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
