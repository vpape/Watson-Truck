using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Watson.Controllers
{
    public class Other_InsuranceController : ApiController
    {
        // GET: api/Other_Insurance
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Other_Insurance/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Other_Insurance
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Other_Insurance/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Other_Insurance/5
        public void Delete(int id)
        {
        }
    }
}
