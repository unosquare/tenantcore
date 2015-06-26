namespace Unosquare.TenantCore
{
    using Microsoft.Owin;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Tenant Resolver class using request's hostname as key to find
    /// a possible Tenant
    /// </summary>
    public class HostNameTenantResolver : ITenantResolver
    {
        protected List<ITenant> Tenants = new List<ITenant>();

        /// <summary>
        /// Empty constructor
        /// </summary>
        public HostNameTenantResolver(string databaseIdentifier = null)
        {
            DatabaseIdentifier = databaseIdentifier;
        }

        /// <summary>
        /// Instances a new resolver with a tenant list
        /// </summary>
        /// <param name="tenants">The list of tenants</param>
        public HostNameTenantResolver(IEnumerable<ITenant> tenants)
        {
            Tenants.AddRange(tenants);
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

        /// <summary>
        /// Adds a new tenant to resolver
        /// </summary>
        /// <param name="tenant">The tenant instance</param>
        public void Add(ITenant tenant)
        {
            Tenants.Add(tenant);
        }

        /// <summary>
        /// Retrieves the Tenants in the resolver
        /// </summary>
        /// <returns>The Tenant's list</returns>
        public List<ITenant> GetTenants()
        {
            return Tenants;
        }

        public string DatabaseIdentifier { get; private set; }
    }
}
