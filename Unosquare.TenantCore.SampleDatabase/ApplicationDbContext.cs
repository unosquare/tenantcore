using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Data.Common;
using System.Data.Entity;
using Unosquare.TenantCore.SampleDatabase.Models;

namespace Unosquare.TenantCore.SampleDatabase
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString, throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(DbConnection connection)
            : base(connection, true)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public static ApplicationDbContext CreateWithOptions(IdentityFactoryOptions<ApplicationDbContext> options,
            IOwinContext context)
        {
            return context.GetDbContext<ApplicationDbContext>();
        }

        public DbSet<Project> Projects { get; set; }

        public void GenerateRandomData()
        {
            var random = new Random();

            for (var i = 0; i <= 20; i++)
            {
                Projects.Add(new Project
                {
                    CreationDate = DateTime.Now,
                    Name = String.Format("Project {0}", i),
                    TenantId = random.Next(1, 4)
                });
            }

            SaveChanges();
        }
    }

    public class DbSeeder : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.GenerateRandomData();
            base.Seed(context);
        }
    }
}