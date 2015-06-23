using System.Collections.Generic;

namespace Unosquare.TenantCore
{
    public interface ITenant
    {
        string Name { get; }

        string ConnectionString { get; }

        string Domain { get; }

        Dictionary<string, string> Properties { get; }
    }
}
