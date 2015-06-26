using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Unosquare.TenantCore.Sample.Providers;
using Unosquare.TenantCore.Sample.Models;

namespace Unosquare.TenantCore.Sample
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            // SetUp Multitenancy with TenantCore
            var tenants = new List<ITenant>
            {
                new Tenant(1, "local", "localhost",
                    @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-Unosquare.TenantCore.Sample-20150623035426.mdf;Initial Catalog=aspnet-Unosquare.TenantCore.Sample-20150623035426;Integrated Security=True"),
                new Tenant(2, "sample", "sample.local",
                    @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-Unosquare.TenantCore.Sample-sample.mdf;Initial Catalog=aspnet-Unosquare.TenantCore.Sample-sample;Integrated Security=True"),
                new Tenant(3, "fake", "fake.local",
                    @"Data Source=(LocalDb)\v11.0;AttachDbFilename=|DataDirectory|\aspnet-Unosquare.TenantCore.Sample-fake.mdf;Initial Catalog=aspnet-Unosquare.TenantCore.Sample-fake;Integrated Security=True")
            };

            app.UseTenantCore(new HostNameTenantResolver(tenants));

            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext<ApplicationDbContext>(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}