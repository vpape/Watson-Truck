﻿using System;
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

        //[Route("api/Family_Info/GetFamilyMember/{User_id:int}/{SSN:string}")]
        //[HttpGet]
        public List<string> GetFamilyMembers(int FamilyMember_id)
        {
            List<string> output = new List<string>();

            foreach (var f in familyMember)
            {
                output.Add(f.FirstName);
            }

            return output;
        }

        // GET: api/Family_Info
        public List<Family_Info> GetFamilyMembers()
        {
            return familyMember;
        }

        // GET: api/Family_Info/5
        public Family_Info GetFamilyMember(int id)
        {
            return familyMember.Where(f => f.FamilyMember_id == id).FirstOrDefault();
        }

        //// PUT: api/Family_Info/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/Family_Info/5
        public void Delete(int id)
        {
        }

        // GET: api/Family_Info
        public void Edit()
        {

        }

        //------------Post Methods-------------------------

        // POST: api/Family_Info
        public void SpouseEnrollment(Family_Info createSpouse)
        {
            if (ModelState.IsValid)
            {
                db.Family_Infoes.Add(createSpouse);
            }

            db.Family_Infoes.Add(createSpouse);

            db.SaveChanges();
        }

        public void SpouseEmployment(Family_Info spouseEmployment)
        {
            if (ModelState.IsValid)
            {
                db.Family_Infoes.Add(spouseEmployment);
            }

            db.Family_Infoes.Add(spouseEmployment);

            db.SaveChanges();
        }

        // POST: api/Family_Info
        public void SpouseContact(Family_Info spouseContact)
        {

        }
    }
}
