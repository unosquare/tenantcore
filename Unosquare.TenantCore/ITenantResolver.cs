using Microsoft.Owin;

namespace Unosquare.TenantCore
{
    public interface ITenantResolver
    {
        ITenant Resolve(IOwinRequest request);
    }
}
