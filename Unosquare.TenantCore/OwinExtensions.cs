namespace Unosquare.TenantCore
{
    using Microsoft.Owin;
    using Owin;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Web;

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
        /// <exception cref="ArgumentNullException"></exception>
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
        /// <exception cref="ArgumentNullException"></exception>
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
        /// <exception cref="ArgumentNullException"></exception>
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
        /// <exception cref="ArgumentNullException"></exception>
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
        /// <exception cref="ArgumentNullException"></exception>
        public static T GetDbContext<T>(this IOwinContext context)
            where T : class
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var tenant = context.GetCurrentTenant();

            if (tenant == null)
            {
                return Activator.CreateInstance<T>();
            }

            return (T)Activator.CreateInstance(typeof(T), tenant.ConnectionString);
        }

        /// <summary>
        /// Retrieves a filtered DbSet by the current Tenant
        /// </summary>
        /// <param name="context">The DbContext</param>
        /// <param name="httpContext">The Http Context</param>
        /// <typeparam name="T">The DbSet type</typeparam>
        /// <returns>The filtered Dbset</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryable<T> GetDbSet<T>(this DbContext context, HttpContext httpContext) where T : class
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            var tenant = httpContext.GetCurrentTenant();

            if (tenant == null || tenant.Resolver == null)
            {
                return context.Set<T>();
            }

            return context.Set<T>().Where(String.Format("{0} = @0", tenant.Resolver.DatabaseIdentifier), tenant.Id);
        }
    }
}
