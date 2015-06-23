using Microsoft.Owin;

namespace TenantCore
{
    public interface ITenantResolver
    {
        ITenant Resolve(IOwinRequest request);
    }
}
