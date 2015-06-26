[![NuGet version](https://badge.fury.io/nu/tenantcore.svg)](http://badge.fury.io/nu/tenantcore)
[![Build Status](https://travis-ci.org/unosquare/tenantcore.svg?branch=master)](https://travis-ci.org/unosquare/tenantcore)
[![Analytics](https://ga-beacon.appspot.com/UA-8535255-2/unosquare/tenantcore/)](https://github.com/igrigorik/ga-beacon)
# TenantCore

TenantCore is an OWIN Middleware that it can help to resolve tenants, a multitenancy middleware, by request's hostname for example or by any resolver. You can create your own Tenant's resolver or use the default one. The tenant can have a database connection string or any property that you need.

To start you need to setup the Resolver and Tenants in your OWIN AppBuilder, for example:

```csharp
public void ConfigureAuth(IAppBuilder app)
{
    // SetUp Multitenancy with TenantCore
    var tenants = new List<ITenant>
    {
        new Tenant("local", "localhost", @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\local.mdf;Initial Catalog=local;Integrated Security=True"),
        new Tenant("sample", "sample.local", @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\sample.mdf;Initial Catalog=sample;Integrated Security=True"),
        new Tenant("fake", "fake.local", @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\fake.mdf;Initial Catalog=fake;Integrated Security=True")
    };
            
    app.UseTenantCore(new HostNameTenantResolver(tenants));

    // Configure the db context and user manager to use a single instance per request
    app.CreatePerOwinContext<ApplicationDbContext>(ApplicationDbContext.Create);
    app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

    // And continue with your middleware and settings
}
```

If you want to have a database per tenant, you need to change your DbContext or IdentityDbContext to have a Create method
with parameters:

```csharp
public static ApplicationDbContext Create(IdentityFactoryOptions<ApplicationDbContext> options,
    IOwinContext context)
{
    return context.GetDbContext<ApplicationDbContext>();
}
```

The GetDbContext is an extension method in TenantCore, it will find the current Tenant and get the ConnectionString. The ConnectionString
will be used with your DbContext, so be sure to have a constructor with connectionstring param like this:

```csharp
public ApplicationDbContext(string connectionString)
            : base(connectionString, throwIfV1Schema: false) {}
```

Now in your ApiController you can use the Tenant or DbContext with some extension methods, for example:

```csharp
[HttpGet, Route("register/{id}")]
public async Task<IHttpActionResult> Register(string id)
{
    // Gets the DbContext related to current Tenant
    var database = HttpContext.Current.GetDbContext<ApplicationDbContext>();
    // Gets the current Tenant
    var tenant = HttpContext.Current.GetCurrentTenant();
}
```
