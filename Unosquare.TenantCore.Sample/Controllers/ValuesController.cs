using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Unosquare.TenantCore;

namespace Unosquare.TenantCore.Sample.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { HttpContext.Current.GetOwinContext().GetCurrentTenant().Name };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
