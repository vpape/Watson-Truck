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
    public class Family_InfoController : System.Web.Mvc.Controller
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        static List<Family_Info> familyMember = new List<Family_Info>();
       
        public Family_InfoController()
        {
            familyMember.Add(new Family_Info { SSN = "0001", FirstName = "Vernon", LastName = "Pape", FamilyMember_id = 1 });
            familyMember.Add(new Family_Info { SSN = "0002", FirstName = "Lynetta", LastName = "Richards", FamilyMember_id = 2 });
            familyMember.Add(new Family_Info { SSN = "0003", FirstName = "LaNita", LastName = "Palmer", FamilyMember_id = 3 });

        }

        //GET: Family_info
        public JsonResult SpouseAndDependentOverview()
        {
            var output = (from f in db.Family_Infoes
                          select new
                          {
                              f.FamilyMember_id,
                              f.SSN,
                              f.FirstName,
                              f.LastName,
                              f.MailingAddress,
                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);

        }

        //GET: Family_info/5
        public JsonResult SpouseAndDependentOverview(int id)
        {
            Family_Info f = db.Family_Infoes
                .Where(i => i.FamilyMember_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        //[Route("api/Family_Info/GetFamilyMember/{User_id:int}/{SSN:string}")]
        //[HttpGet]
        //public List<string> GetFamilyMembers(int FamilyMember_id)
        //{
        //    List<string> output = new List<string>();

        //    foreach (var f in familyMember)
        //    {
        //        output.Add(f.FirstName);
        //    }

        //    return output;
        //}

        //// GET: api/Family_Info
        //public List<Family_Info> FamilyMemberOverview()
        //{
        //    return familyMember;
        //}

        //// GET: api/Family_Info/5
        //public Family_Info FamilyMemberOverview(int? id)
        //{
        //    return familyMember.Where(f => f.FamilyMember_id == id).FirstOrDefault();
        //}
        //----------------------------------------------------------------------------------------

        //stopping point

        public JsonResult SpouseEnrollment(int id, string MartialStatus)
        {         

            var output = (from f in db.Family_Infoes
                          select new
                          {
                              f.RelationshipToInsured,
                              f.FirstName,
                              f.LastName,
                              f.DateOfBirth,
                              f.Sex,
                              f.MailingAddress,
                              f.PhysicalAddress,
                              f.City,
                              f.State,
                              f.ZipCode,
                              f.PhoneNumber,
                              f.CellPhone,
                          });

            ViewBag.Employee_id = id;
            ViewBag.spouseExist = true;

            Employee employee = db.Employees.Find(id);
            if (employee.MartialStatus == "Single")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Single";
            }
            else if (employee.MartialStatus == "SinglewDep")
            {
                ViewBag.spouseExist = false;
                ViewBag.RelationshipToInsured = "Spouse";
            }
            else
            {
                ViewBag.RelationshipToInsured = "Dependent";
            }

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }



        //GET: api/Family_Info/5
        public Family_Info SpouseEnrollment(int id)
        {
            return familyMember.Where(i => i.FamilyMember_id == id).FirstOrDefault();
        }

        //public HttpResponseMessage SpouseEnrollment() {

        //    var path = "~/SpouseEnrollment.html";
        //    var response = new HttpResponseMessage();
        //    response.Content = new StringContent(File.ReadAllText(path));
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        //    return response;
        //}

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
            return familyMember.Where(i => i.FamilyMember_id == id).FirstOrDefault();
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
