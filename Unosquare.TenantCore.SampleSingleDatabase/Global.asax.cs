using System.Web.Http;

namespace Unosquare.TenantCore.SampleSingleDatabase
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
