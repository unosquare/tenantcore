using System.Threading.Tasks;
using Microsoft.Owin;

namespace TenantCore
{
    public class TenantCoreMiddleware : OwinMiddleware
    {
        public static string OwinPropertyName = "Tenant";

        protected ITenantResolver Resolver;

        public TenantCoreMiddleware(OwinMiddleware next, ITenantResolver resolver) : base(next)
        {
            Resolver = resolver;
        }

        public async override Task Invoke(IOwinContext context)
        {
            var tenant = Resolver.Resolve(context.Request);
            context.Set(OwinPropertyName, tenant);

            await Next.Invoke(context);
        }
    }
}
