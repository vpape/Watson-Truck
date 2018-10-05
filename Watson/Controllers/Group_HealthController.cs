using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Watson.Controllers
{
    public class Group_HealthController : ApiController
    {
        // GET: api/Group_Health
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Group_Health/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Group_Health
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Group_Health/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Group_Health/5
        public void Delete(int id)
        {
        }
    }
}
