using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Unosquare.TenantCore.SampleDatabase;
using Unosquare.TenantCore.SampleDatabase.Models;

namespace Unosquare.TenantCore.SampleSingleDatabase.Controllers
{
    [RoutePrefix("/project/")]
    public class ProjectController : ApiController
    {
        // GET api/values
        public IEnumerable<Project> Get()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.GetDbSet<Project>(HttpContext.Current).ToList();
            }
        }

        // GET api/values/5
        public Project Get(long id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.GetDbSet<Project>(HttpContext.Current).FirstOrDefault(x => x.Id == id);
            }
        }
    }
}