using Microsoft.Owin;
using Owin;
using Startup = PhotoGallery.WebUI.Startup;

[assembly: OwinStartup(typeof(Startup))]
namespace PhotoGallery.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
