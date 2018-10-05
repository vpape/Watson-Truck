using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Watson.Controllers
{
    public class Life_InsuranceController : ApiController
    {
        // GET: api/Life_Insurance
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Life_Insurance/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Life_Insurance
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Life_Insurance/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Life_Insurance/5
        public void Delete(int id)
        {
        }
    }
}
