using System;
using Microsoft.Owin;
using Owin;

namespace TenantCore
{
    /// <summary>
    /// 
    /// </summary>
    public static class OwinExtensions
    {
        public static IAppBuilder UseTenantCore(this IAppBuilder app, ITenantResolver resolver)
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            if (resolver == null)
            {
                throw new ArgumentNullException("resolver");
            }

            return app.Use(typeof(TenantCoreMiddleware), resolver);
        }

        public static ITenant GetCurrentTenant(this IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.Get<ITenant>(TenantCoreMiddleware.OwinPropertyName);
        }
    }
}
