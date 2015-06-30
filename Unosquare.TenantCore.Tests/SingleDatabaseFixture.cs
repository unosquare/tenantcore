using Effort;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using Owin;
using System.Collections.Generic;
using System.Linq;
using Unosquare.TenantCore.SampleDatabase;

namespace Unosquare.TenantCore.Tests
{
    [TestFixture]
    public class SingleDatabaseFixture
    {
        private ApplicationDbContext _context;
        private TestServer _server;
        private ITenantResolver _resolver;

        [SetUp]
        public void Setup()
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();

            var connection = DbConnectionFactory.CreateTransient();
            _context = new ApplicationDbContext(connection);
            _context.GenerateRandomData();

            _resolver = new HostNameTenantResolver(new List<ITenant>
            {
                new Tenant(1, "local", "localhost"),
                new Tenant(2, "sample", "sample.local")
            }, "TenantId");

            _server = TestServer.Create(app =>
            {
                app.UseTenantCore(_resolver);

                app.Run(context =>
                {
                    var tenant = context.GetCurrentTenant();

                    return context.Response.WriteAsync(tenant.Name);
                });
            });
        }

        [Test]
        public async void GetTenant()
        {
            var response = await _server.HttpClient.GetAsync("/");

            Assert.IsTrue(response.IsSuccessStatusCode);

            var content = await response.Content.ReadAsStringAsync();

            Assert.IsNotNullOrEmpty(content);
            Assert.AreEqual(content, _resolver.GetTenants().First().Name);
        }
    }
}