namespace Unosquare.TenantCore
{
    using Microsoft.Owin;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Tenant Resolver class using request's hostname as key to find
    /// a possible Tenant
    /// </summary>
    public class HostNameTenantResolver : TenantResolverBase, ITenantResolver
    {
        /// <summary>
        /// Instances a new Resolver without tenants
        /// </summary>
        /// <param name="databaseIdentifier">The Database Identifier</param>
        public HostNameTenantResolver(string databaseIdentifier = null) : base(databaseIdentifier)
        {

        }

        /// <summary>
        /// Instances a new resolver with a tenant list
        /// </summary>
        /// <param name="tenants">The list of tenants</param>
        /// <param name="databaseIdentifier">The Database Identifier</param>
        public HostNameTenantResolver(IEnumerable<ITenant> tenants, string databaseIdentifier = null)
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
            var host = request.Uri.Host;

            return Tenants.FirstOrDefault(x => x.Domain == host);
        }
    }
}