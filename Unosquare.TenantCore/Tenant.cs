namespace Unosquare.TenantCore
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a common Tenant
    /// </summary>
    public  class Tenant : ITenant
    {
        /// <summary>
        /// Tenant's name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The Tenant connection string
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// The Tenant domains
        /// </summary>
        public string Domain { get; private set; }

        /// <summary>
        /// Properties collection
        /// </summary>
        public Dictionary<string, string> Properties { get; private set; }

        /// <summary>
        /// Creates a new Tenant
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="domain">The related Domain</param>
        /// <param name="connectionString">Optional ConnectiongString</param>
        public Tenant(string name, string domain, string connectionString = null)
        {
            Name = name;
            Domain = domain;
            ConnectionString = connectionString;
            Properties = new Dictionary<string, string>();
        }
    }
}