using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Watson.Models;

namespace Watson.Controllers
{
    public class Family_InfoController : ApiController
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        static List<Family_Info> familyMember = new List<Family_Info>();

        public Family_InfoController()
        {
            familyMember.Add(new Family_Info { FirstName = "Cindy", FamilyMember_id = 1 });
            
        }

        // GET: api/Family_Info
        public List<Family_Info> Get()
        {
            return db.Family_Infoes.ToList();
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

        // POST: api/Family_Info
        public void SpouseContact(Family_Info spouseContact)
        {

        }
    }
}
