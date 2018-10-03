using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Watson.Controllers
{
    public class Family_InfoController : ApiController
    {
        // GET: api/Family_Info
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Family_Info/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Family_Info
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Family_Info/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Family_Info/5
        public void Delete(int id)
        {
        }
    }
}
