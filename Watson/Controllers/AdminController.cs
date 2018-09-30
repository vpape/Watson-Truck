using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;
using Watson.Models;

//ask if i need admin and employee model since i created 
//two separate controllers or can i just use one model- employee

namespace Watson.Controllers
{
    /// <summary>
    /// This is where I give you all the information about my employees
    /// </summary>
    //[Authorize]
    public class AdminController : ApiController
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        static List<Employee> admin = new List<Employee>();

        public AdminController()
        {
            admin.Add(new Employee { SSN = "", FirstName = "Vernon", LastName = "Pape", EmployeeRole = "", JobTitle = "", User_id = 1});
            admin.Add(new Employee { SSN = "", FirstName = "Lynetta", LastName = "Richards", EmployeeRole = "", JobTitle = "", User_id = 2});
            admin.Add(new Employee { SSN = "", FirstName = "LaNita", LastName = "Palmer", EmployeeRole = "", JobTitle = "", User_id = 3});
        }

        //public AdminController(string SSN, string FirstName, string LastName, string EmployeeRole, string JobTitle, int id)
        //{
        //    admin.Add(new Employee { SSN = "", FirstName = "", LastName = "", EmployeeRole = "", JobTitle = "", User_id = id });
        //}

        // GET: api/Admin
        public List<Employee> Index()
        {
            return db.Employees.ToList();
        }

        /// <summary>
        /// Gets a list of the admins
        /// </summary>
        /// <param name="User_id">The unique identifier for this person</param>
        /// <param name="SSN">We want to know their employee#</param>
        /// <returns>A list of admin Emp#, FN, LN, EmpRole, & JobTitle</returns>
        [Route("api/Employee/GetEmployees/{User_id:int}/{SSN:string}")]
        [HttpGet]
        public List<string> GetEmployees(int User_id, string SSN)
        {
            List<string> output = new List<string>();

            foreach (var a in admin)
            {
                output.Add(a.SSN);
                output.Add(a.FirstName);
                output.Add(a.LastName);
                output.Add(a.EmployeeRole);
                output.Add(a.JobTitle);
                //output.Add(a.isActive);
            }

            return output;
        }

        // GET: api/Admin
        public List<Employee> GetAdmin()
        {
            return admin;
        }

        // GET: api/Admin/5
        public Employee GetAdmin(int id)
        {
            return admin.Where(x => x.User_id == id).FirstOrDefault();
        }

        // POST: api/Admin
        public void CreateAdmin(Employee value)
        {
            admin.Add(value);
        }

        // PUT: api/Admin/5
        //public void UpdateAdmin(int id, Employee value)
        //{
        //    admin[id] = value;
        //}

        // DELETE: api/Admin/5
        public void DeleteAdmin(int id)
        {
            admin.RemoveAt(id);
        }       
    }
}
