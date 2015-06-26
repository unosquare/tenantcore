using Microsoft.Owin;
using Owin;
using Unosquare.TenantCore.SampleSingleDatabase;

[assembly: OwinStartup(typeof(Startup))]

namespace Unosquare.TenantCore.SampleSingleDatabase
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
