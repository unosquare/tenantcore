using System.Collections.Generic;

namespace Unosquare.TenantCore
{
    public  class Tenant : ITenant
    {
        public string Name { get; private set; }
        public string ConnectionString { get; private set; }
        public string Domain { get; private set; }
        public Dictionary<string, string> Properties { get; private set; }

        public Tenant(string name, string domain, string connectionString = null)
        {
            Name = name;
            Domain = domain;
            ConnectionString = connectionString;
            Properties = new Dictionary<string, string>();
        }
    }
}
