namespace Unosquare.TenantCore
{
    using Microsoft.Owin;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;

    /// <summary>
    /// You need to setup your route like "api/{tenant}/{controller}/{id}"
    /// in order to receive the Tenant value.
    /// </summary>
    public class RouteTenantResolver : TenantResolverBase, ITenantResolver
    {
        /// <summary>
        /// Defines the RouteName identifier
        /// </summary>
        public const string RouteName = "tenant";

        /// <summary>
        /// Instances a new resolver with a tenant list
        /// </summary>
        /// <param name="tenants">The list of tenants</param>
        /// <param name="databaseIdentifier">The Database Identifier</param>
        public RouteTenantResolver(IEnumerable<ITenant> tenants, string databaseIdentifier = null)
            : base(tenants, databaseIdentifier)
        {
        }

        /// <summary>
        /// Resolves a tenant request using hostname
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>The tenant or null</returns>
        public ITenant Resolve(IOwinRequest request)
        {
            var httpContext = request.Context.Get<HttpContextWrapper>("System.Web.HttpContextBase");
            var routeData = RouteTable.Routes.GetRouteData(httpContext);

            if (routeData == null || routeData.Values.ContainsKey(RouteName) == false)
                return Tenants.First();

            var tenant = Tenants.FirstOrDefault(x => x.Name == routeData.Values[RouteName].ToString());

            return tenant ?? Tenants.First();
        }
    }
}