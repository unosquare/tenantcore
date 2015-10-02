namespace Unosquare.TenantCore
{
    using System;
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

        /// <summary>
        /// The Tenant Resolver
        /// </summary>
        protected ITenantResolver Resolver;

        /// <summary>
        /// The Callback
        /// </summary>
        protected Func<ITenant, bool> Callback;

        /// <summary>
        /// Initialize the Middleware with a Tenant's resolver
        /// </summary>
        /// <param name="next">The next Middleware</param>
        /// <param name="resolver">The Tenant's resolver</param>
        /// <param name="callback">The Callback action</param>
        public TenantCoreMiddleware(OwinMiddleware next, ITenantResolver resolver, Func<ITenant, bool> callback = null)
            : base(next)
        {
            Resolver = resolver;
            Callback = callback;
        }

        /// <summary>
        /// Method to receive a new request
        /// </summary>
        /// <param name="context">The OWIN Context</param>
        /// <returns>The next Task</returns>
        public override async Task Invoke(IOwinContext context)
        {
            var tenant = Resolver.Resolve(context.Request);

            if (tenant != null)
            {
                tenant.Resolver = Resolver;

                if (Callback != null)
                {
                    var callBackResult = Callback(tenant);

                    if (callBackResult == false)
                    {
                        // Do something maybe
                    }
                }
            }

            context.Set(OwinPropertyName, tenant);

            await Next.Invoke(context);
        }
    }
}