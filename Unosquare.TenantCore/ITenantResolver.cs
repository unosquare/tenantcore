namespace Unosquare.TenantCore
{
    using System.Collections.Generic;
    using Microsoft.Owin;

    /// <summary>
    /// Public Tenant Resolver interface
    /// </summary>
    public interface ITenantResolver
    {
        /// <summary>
        /// This method should take a Owin Request and
        /// resolve the possible Tenant
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>The tenant</returns>
        ITenant Resolve(IOwinRequest request);

        /// <summary>
        /// Retrieves the Tenants in the resolver
        /// </summary>
        /// <returns>The Tenant's list</returns>
        List<ITenant> GetTenants();
    }
}
