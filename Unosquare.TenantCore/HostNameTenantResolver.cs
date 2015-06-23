using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;

namespace Unosquare.TenantCore
{
    public class HostNameTenantResolver : ITenantResolver
    {
        protected List<ITenant> Tenants = new List<ITenant>();

        public HostNameTenantResolver()
        {
            
        }

        public HostNameTenantResolver(IEnumerable<ITenant> tenants)
        {
            Tenants.AddRange(tenants);
        }

        public ITenant Resolve(IOwinRequest request)
        {
            var host = request.Uri.Host;

            return Tenants.FirstOrDefault(x => x.Domain == host);
        }

        public void Add(Tenant tenant)
        {
            Tenants.Add(tenant);
        }
    }
}
