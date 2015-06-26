namespace Unosquare.TenantCore
{
    using System.Collections.Generic;

    /// <summary>
    /// Public interface to create Tenant instances
    /// </summary>
    public interface ITenant
    {
        /// <summary>
        /// Tenant's Id
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Tenant's name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The Tenant connection string
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// The Tenant domains
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Properties collection
        /// </summary>
        Dictionary<string, string> Properties { get; }

        /// <summary>
        /// The Resolver instance
        /// </summary>
        ITenantResolver Resolver { get; set; }
    }
}
