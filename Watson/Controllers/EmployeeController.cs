using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Watson.Models;


//ask if i need admin and employee model since i created 
//two controllers or can i just use one model- employee

namespace Watson.Controllers
{   
    //[Authorize]
    public class EmployeeController : ApiController
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        static List<Employee> employee = new List<Employee>();

        public EmployeeController()
        {
            employee.Add(new Employee { SSN = "", FirstName = "", LastName = "", EmployeeRole = "", JobTitle = "", User_id = 1});
            employee.Add(new Employee { SSN = "", FirstName = "Vernon", LastName = "Pape", EmployeeRole = "", JobTitle = "", User_id = 2 });
            employee.Add(new Employee { SSN = "", FirstName = "Lynetta", LastName = "Richards", EmployeeRole = "", JobTitle = "", User_id = 3 });
            employee.Add(new Employee { SSN = "", FirstName = "LaNita", LastName = "Palmer", EmployeeRole = "", JobTitle = "", User_id = 4 });
        }

        //public EmployeeController(string SSN, string FirstName, string LastName, string EmployeeRole, string JobTitle, int id)
        //{
        //    employee.Add(new Employee { SSN = "", FirstName = "", LastName = "", EmployeeRole = "", JobTitle = "", User_id = id });
        //}

        // GET: Employees
        public List<Employee> Index()
        {
            return db.Employees.ToList();
        }

        /// <summary>
        /// Gets a list of the employees
        /// </summary>
        /// <param name="User_id">The unique identifier for this person</param>
        /// <param name="SSN">We want to know their employee#</param>
        /// <returns>A list of employees Emp#, FN, LN, EmpRole, & JobTitle</returns>
        //[Route("api/Employee/GetEmployees/{User_id:int}/{SSN:string}")]
        //[HttpGet]
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
                //output.Add(e.isActive);
            }

            return output;
        }

        // GET: api/Employee
        public List<Employee> GetEmployee()
        {
            return employee;
        }

        // GET: api/Employee/5
        public Employee GetEmployee(int id)
        {
            return employee.Where(e => e.User_id == id).FirstOrDefault();
        }


        // PUT: api/Employee/5
        //public void UpdateEmployee(int id, Employee value)
        //{
        //    employee[id] = value;
        //}

        // DELETE: api/Employee/5
        public void DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);

            db.Employees.Remove(employee);
            db.SaveChanges();

            //db.DeleteEmployeeAndDependents(id);
           
        }

        //---------------POST Methods---------------//


        // POST: api/Employee
        public void CreateEmployee(Employee create)
        {
            employee.Add(create);
        }

        // POST: api/Employee
        public void Contact(Employee contact)
        {
            employee.Add(contact);
        }
    }
}
