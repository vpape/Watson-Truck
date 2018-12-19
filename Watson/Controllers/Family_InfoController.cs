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
           
        }

        //GET: Family_info
        public JsonResult SpouseAndDependentOverview()
        {
            var output = (from f in db.Family_Info
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
        public JsonResult SpouseAndDependentOverview(int? id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        //[Route("api/Family_Info/GetFamilyMember/{FamilyMember_id:int}/{SSN:string}")]
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

        // GET: api/Family_Info/5
        //public void Index(int? id)
        //{
        //    Family_Info fMember = db.Family_Infoes.Find(id);
        //}
        //----------------------------------------------------------------------------------------


        public JsonResult SpouseEnrollment(int id, string MaritalStatus)
        {

            var output = (from f in db.Family_Info
                          select new
                          {
                              f.RelationshipToInsured,
                              //Employee Number
                              //Employee Name
                              f.FirstName,
                              f.LastName,
                              f.DateOfBirth,
                              f.Sex,

                          });

            //Realtionship Status for Spouse and Dependent Enrollment View isn't working
            //ViewBag.Employee_id = id;
            //ViewBag.spouseExist = true;            
            //ViewBag.MartialStatus = MartialStatus;

            //Employee employee = db.Employees.Find(id);

            //if (employee.MartialStatus == "Single")
            //{
            //    ViewBag.spouseExist = false;
            //    ViewBag.RelationshipToInsured = "Single";
            //}
            //else if (employee.MartialStatus == "SinglewDep")
            //{
            //    ViewBag.spouseExist = false;
            //    ViewBag.RelationshipToInsured = "Spouse";
            //}
            //else
            //{
            //    ViewBag.RelationshipToInsured = "Dependent";
            //}

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SpouseEnrollment(int? id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == id)
                .SingleOrDefault();

            db.Family_Info.Add(f);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);

        }


        //----------------------------------------------------------------------------------------
        //GET: api/Family_Info/5
        //public Family_Info SpouseEnrollment(int id)
        //{
        //    return familyMember.Where(i => i.FamilyMember_id == id).FirstOrDefault();
        //}

        //POST: api/Family_Info
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public void SpouseEnrollment([Bind(Include = "")]Family_Info createSpouse)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Family_Infoes.Add(createSpouse);
        //        db.SaveChanges();
        //    }
        //}
        //----------------------------------------------------------------------------------------

        public JsonResult SpouseContact()
        {
            var output = (from f in db.Family_Info
                          select new
                          {
                              f.MailingAddress,
                              f.PhysicalAddress,
                              f.City,
                              f.State,
                              f.ZipCode,
                              f.County,
                              f.EmailAddress,
                              f.PhoneNumber,
                              f.CellPhone,
                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SpouseContact(int? id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == id)
                .SingleOrDefault();

            db.Family_Info.Add(f);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        //GET: api/Family_Info/5
        //public Family_Info SpouseContact(int id)
        //{
        //    return familyMember.Where(i => i.FamilyMember_id == id).FirstOrDefault();
        //}

        //POST: api/Family_Info
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public void SpouseContact([Bind(Include = "")]Family_Info spouseContact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Family_Infoes.Add(spouseContact);
        //        db.SaveChanges();
        //    }
        //}
        //----------------------------------------------------------------------------------------


        public JsonResult SpouseEmployment()
        {
            var output = (from f in db.Family_Info
                          select new
                          {

                          });

            return Json(new { data = output }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SpouseEmployment(int? id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == id)
                .SingleOrDefault();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }


        //----------------------------------------------------------------------------------------
        //GET: api/Family_Info/5
        //public Family_Info SpouseEmployment(int id)
        //{
        //    return familyMember.Where(i => i.FamilyMember_id == id).FirstOrDefault();
        //}

        //POST: api/Family_Info
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public void SpouseEmployment([Bind(Include = "")]Family_Info spouseEmployment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Family_Infoes.Add(spouseEmployment);
        //        db.SaveChanges();
        //    }
        //}
        //----------------------------------------------------------------------------------------


        // GET: Family_Info
        public JsonResult EditSpouse()
        {
            var output = (from f in db.Family_Info
                          select new
                          {

                          });

            return Json(new { data = output }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        // GET: Family_Info/5
        public JsonResult EditSpouse(int? id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == id)
                .SingleOrDefault();

            db.Family_Info.Add(f);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        //----------------------------------------------------------------------------------------
        // GET: api/Family_Info/5
        //[System.Web.Http.Route("api/Family_Info/EditSpouse/{FamilyMember_id:int}")]
        //[System.Web.Http.HttpGet]
        //public ActionResult Edit(int? id)
        //{        
        //    Family_Info familyMember = db.Family_Infoes.Find(id);

        //    return View(familyMember);
        //}

        // POST: api/Family_Info
        //[System.Web.Http.Route("api/Family_Info/EditSpouse")]
        //[System.Web.Http.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditSpouse([Bind(Include = "User_id,CurrentEmployer,SSN,FirstName,MiddleName,LastName,DateOfBirth," +
        //    "Sex,MartialStatus")] Family_Info editSpouse)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(editSpouse).State = System.Data.Entity.EntityState.Modified;
        //        db.SaveChanges();
        //    }

        //    return View(editSpouse);
        //}

        // GET: api/Family_Info
        //public void Edit(int? id)
        //{
        //    Family_Info fMember = db.Family_Infoes.Find(id);
        //}
        //----------------------------------------------------------------------------------------

        // GET: Family_Info/5
        public JsonResult SpouseDetail(int? id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == id)
                .FirstOrDefault();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);

        }

        //----------------------------------------------------------------------------------------
        //GET: api/Family_Info/5
        //[System.Web.Http.Route("api/Family_Info/Detail/{FamilyMember_id:int}")]
        //[System.Web.Http.HttpGet]
        //public Family_Info Detail(int? id)
        //{
        //    return family_info.Where(f => f.FamilyMember_id == id).FirstOrDefault();
        //}
        //----------------------------------------------------------------------------------------

        // GET: Family_Info
        public JsonResult DeleteSpouse()
        {
            var output = (from f in db.Family_Info
                          select new
                          {

                          });

            return Json(new { data = output }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        // GET: Family_Info/5
        public JsonResult DeleteSpouse(int? id)
        {
            Family_Info f = db.Family_Info
                .Where(i => i.FamilyMember_id == id)
                .SingleOrDefault();


            //db.DeleteEmployeeAndDependents(id);
            db.Family_Info.Remove(f);
            db.SaveChanges();

            return Json(new { data = "success" }, "application/javascript", JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //----------------------------------------------------------------------------------------        
        //DELETE: api/Family_Info/5
        //[System.Web.Http.Route("api/Family_Info/Delete/{FamilyMember_id:int}")]
        //[System.Web.Http.HttpGet]
        //public ActionResult DeleteSpouse(int? id)
        //{
        //    if(id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Family_Info f = db.Family_Infoes.Find(id);

        //    if(familyMember == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(familyMember);


        //    db.Family_Infoes.Remove(f);
        //    db.SaveChanges();

        //}

        // POST: api/Family_Info/5
        //[System.Web.Http.HttpPost, System.Web.Http.ActionName("DeleteSpouse")]
        //[ValidateAntiForgeryToken]
        //public void DeleteSpouse(int id)
        //{
        //    Family_Info f = db.Family_Infoes.Find(id);
        //    db.Family_Infoes.Remove(f);
        //    db.SaveChanges();

        //    db.DeleteEmployeeAndDependents(id);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        //----------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------
        //GET: api/Family_Info/5
        //public Family_Info DependentEnrollment(int id)
        //{
        //    return familyMember.Where(i => i.FamilyMember_id == id).FirstOrDefault();
        //}

        ////POST: api/Family_Info
        //public void DependentEnrollment([Bind(Include = "")] Family_Info createDependent)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Family_Infoes.Add(createDependent);
        //        db.SaveChanges();
        //    }

        //}
        //----------------------------------------------------------------------------------------










    }

}
