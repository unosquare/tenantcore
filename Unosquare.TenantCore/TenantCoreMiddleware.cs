namespace Unosquare.TenantCore
{
    using System.Threading.Tasks;
    using Microsoft.Owin;

    /// <summary>
    /// Owin Middlware to connect TenantCore to Owin
    /// </summary>
    public class TenantCoreMiddleware : OwinMiddleware
    {
        /// <summary>
        /// Environment variable name
        /// </summary>
        public static string OwinPropertyName = "Tenant";

        protected ITenantResolver Resolver;

        /// <summary>
        /// Initialize the Middleware with a Tenant's resolver
        /// </summary>
        /// <param name="next">The next Middleware</param>
        /// <param name="resolver">The Tenant's resolver</param>
        public TenantCoreMiddleware(OwinMiddleware next, ITenantResolver resolver) : base(next)
        {
            Resolver = resolver;
        }

        /// <summary>
        /// Method to receive a new request
        /// </summary>
        /// <param name="context">The OWIN Context</param>
        /// <returns>The next Task</returns>
        public async override Task Invoke(IOwinContext context)
        {
            var tenant = Resolver.Resolve(context.Request);
            context.Set(OwinPropertyName, tenant);

            await Next.Invoke(context);
        }
    }
}
