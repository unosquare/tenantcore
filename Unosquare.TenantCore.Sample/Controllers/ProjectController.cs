using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unosquare.TenantCore.SampleDatabase;
using Unosquare.TenantCore.SampleDatabase.Models;

namespace Unosquare.TenantCore.Sample.Controllers
{
    [RoutePrefix("/project/")]
    public class ProjectController : ApiController
    {
        // GET api/values
        public IEnumerable<Project> Get()
        {
            using (var context = HttpContext.Current.GetDbContext<ApplicationDbContext>())
            {
                return context.Projects.ToList();
            }
        }

        // GET api/values/5
        public Project Get(long id)
        {
            using (var context = HttpContext.Current.GetDbContext<ApplicationDbContext>())
            {
                return context.Projects.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}