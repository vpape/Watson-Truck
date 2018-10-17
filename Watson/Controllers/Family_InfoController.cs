using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
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
        public List<Family_Info> FamilyMemberOverview()
        {
            return familyMember;
        }

        // GET: api/Family_Info/5
        public Family_Info FamilyMemberOverview(int? id)
        {
            return familyMember.Where(f => f.FamilyMember_id == id).FirstOrDefault();
        }

        //GET: api/Family_Info/5
        public Family_Info SpouseEnrollment(int id)
        {
            return familyMember.Where(s => s.FamilyMember_id == id).FirstOrDefault();
        }

        //POST: api/Family_Info
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public void SpouseEnrollment([Bind(Include = "")]Family_Info createSpouse)
        {
            if (ModelState.IsValid)
            {
                db.Family_Infoes.Add(createSpouse);
                db.SaveChanges();
            }
        }

        //GET: api/Family_Info/5
        public Family_Info DependentEnrollment(int id)
        {
            return familyMember.Where(d => d.FamilyMember_id == id).FirstOrDefault();
        }

        //public ActionResult DependentEnrollment(int? id, string MartialStatus)
        //{
        //    ViewBag.Employee_id = id;
        //    ViewBag.spouseExist = true;

        //    Employee employee = db.Employees.Find(id);

        //    if (employee.MartialStatus == "Single")
        //    {
        //        ViewBag.spouseExist = false;
        //        ViewBag.RelationshipToInsured = "Single";
        //    }
        //    else if (employee.MartialStatus == "SinglewDep")
        //    {
        //        ViewBag.RelationshipToInsured = "Spouse";
        //    }
        //    else
        //    {
        //        ViewBag.RelationshipToInsured = "Dependent";
        //    }

        //    return ViewBag();
        //}

        //POST: api/Family_Info
        public void DependentEnrollment([Bind(Include = "")] Family_Info createDependent)
        {
            if (ModelState.IsValid)
            {
                db.Family_Infoes.Add(createDependent);
                db.SaveChanges();
            }
            
        }


        //// PUT: api/Family_Info/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE: api/Family_Info/5
        public void Delete(int? id)
        {
            Family_Info fMember = db.Family_Infoes.Find(id);

            db.Family_Infoes.Remove(fMember);
            db.SaveChanges();
        }

        // GET: api/Family_Info/5
        public void Index(int? id)
        {
            Family_Info fMember = db.Family_Infoes.Find(id);
        }

        // GET: api/Family_Info
        public void Edit(int? id)
        {
            Family_Info fMember = db.Family_Infoes.Find(id);
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
