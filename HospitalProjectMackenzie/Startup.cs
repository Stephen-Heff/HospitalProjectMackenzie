using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HospitalProjectMackenzie.Startup))]
namespace HospitalProjectMackenzie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
