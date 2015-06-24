namespace Unosquare.TenantCore
{
    using System;
    using System.Web;
    using Microsoft.Owin;
    using Owin;

    /// <summary>
    /// Extensions methods
    /// </summary>
    public static class OwinExtensions
    {
        /// <summary>
        /// Add TenantCore to an AppBuilder
        /// </summary>
        /// <param name="app">The AppBuilder instance</param>
        /// <param name="resolver">The Tenant Resolver instance</param>
        /// <returns>AppBuilder with TenantCore</returns>
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

        /// <summary>
        /// Retrieves the current Tenant from a HttpContext
        /// </summary>
        /// <param name="context">The Http context</param>
        /// <returns>The Tenant</returns>
        public static ITenant GetCurrentTenant(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.GetOwinContext().GetCurrentTenant();
        }

        /// <summary>
        /// Retrieves the current Tenant from a Owin Context
        /// </summary>
        /// <param name="context">The Owin context</param>
        /// <returns>The Tenant</returns>
        public static ITenant GetCurrentTenant(this IOwinContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.Get<ITenant>(TenantCoreMiddleware.OwinPropertyName);
        }

        /// <summary>
        /// Retrieves the current database (using Tenant Connectionstring)
        /// </summary>
        /// <typeparam name="T">The DbContext Type</typeparam>
        /// <param name="context">The Http Context</param>
        /// <returns>A DbContext</returns>
        public static T GetDbContext<T>(this HttpContext context)
            where T : class
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.GetOwinContext().GetDbContext<T>();
        }

        /// <summary>
        /// Retrieves the current database (using Tenant Connectionstring)
        /// </summary>
        /// <typeparam name="T">The DbContext Type</typeparam>
        /// <param name="context">The Owin Context</param>
        /// <returns>A DbContext</returns>
        public static T GetDbContext<T>(this IOwinContext context)
            where T : class
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var tenant = context.GetCurrentTenant();

            if (tenant == null)
                return Activator.CreateInstance<T>();

            return (T)Activator.CreateInstance(typeof(T), tenant.ConnectionString);
        }
    }
}
