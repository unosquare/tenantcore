namespace Unosquare.TenantCore
{
    using System.Collections.Generic;

    /// <summary>
    /// Base Tenant Resolver functionality with internal Tenant List
    /// </summary>
    public abstract class TenantResolverBase
    {
        /// <summary>
        /// The Tenants repository
        /// </summary>
        protected List<ITenant> Tenants = new List<ITenant>();

        /// <summary>
        /// Instances a new Resolver without tenants
        /// </summary>
        /// <param name="databaseIdentifier">The Database Identifier</param>
        protected TenantResolverBase(string databaseIdentifier = null)
        {
            DatabaseIdentifier = databaseIdentifier;
        }

        /// <summary>
        /// Instances a new resolver with a tenant list
        /// </summary>
        /// <param name="tenants">The list of tenants</param>
        /// <param name="databaseIdentifier">The Database Identifier</param>
        protected TenantResolverBase(IEnumerable<ITenant> tenants, string databaseIdentifier = null)
        {
            Tenants.AddRange(tenants);
            DatabaseIdentifier = databaseIdentifier;
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

        /// <summary>
        /// Retrieves the Database Identifier
        /// </summary>
        public string DatabaseIdentifier { get; private set; }
    }
}
