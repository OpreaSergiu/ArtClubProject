using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtClub.Startup))]
namespace ArtClub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
