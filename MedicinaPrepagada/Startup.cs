using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicinaPrepagada.Startup))]
namespace MedicinaPrepagada
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
