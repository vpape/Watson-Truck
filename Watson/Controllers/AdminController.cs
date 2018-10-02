using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
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

        static List<Employee> employee = new List<Employee>();

        public AdminController()
        {
            employee.Add(new Employee { SSN = "", FirstName = "Vernon", LastName = "Pape", EmployeeRole = "", JobTitle = "", User_id = 1 });
            employee.Add(new Employee { SSN = "", FirstName = "Lynetta", LastName = "Richards", EmployeeRole = "", JobTitle = "", User_id = 2 });
            employee.Add(new Employee { SSN = "", FirstName = "LaNita", LastName = "Palmer", EmployeeRole = "", JobTitle = "", User_id = 3 });
        }

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
        
        [System.Web.Http.Route("api/Admin/GetEmployees/{User_id:int}/{SSN:string}")]
        [System.Web.Http.HttpGet]
        public List<string> GetEmployees(int User_id, string SSN)
        {
            List<string> output = new List<string>();

            foreach (var e in employee)
            {
                output.Add(e.SSN);
                output.Add(e.FirstName);
                output.Add(e.LastName);
                output.Add(e.EmployeeRole);
                output.Add(e.JobTitle);
                //output.Add(a.isActive);
            }

            return output;
        }
        
        // GET: api/Admin
        public List<Employee> GetEmployee()
        {
            return employee;
        }

        // GET: api/Admin/5
        public Employee GetEmployee(int id)
        {
            return employee.Where(x => x.User_id == id).FirstOrDefault();
        }

        // GET: api/Admin/5
        public Employee EmployeeDetail(int? id)
        {
         
            var employee =db.Employees.Find(id);

            return employee;
        }

        // GET: api/Admin/5
        public List<Employee> EditEmployee(int? id)
        {
            
            return employee;
        }

        // PUT: api/Admin/5
        public void UpdateEmployee(int id, Employee update)
        {
            employee[id] = update;
            
        }

        // DELETE: api/Admin/5
        public void DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();

        }

        // POST: api/Admin
        public void CreateEmployee(Employee create)
        {
            employee.Add(create);
        }

        // POST: api/Admin/5
    }
}
