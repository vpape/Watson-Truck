using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        }

        //public EmployeeController(string SSN, string FirstName, string LastName, string EmployeeRole, string JobTitle, int id)
        //{
        //    employee.Add(new Employee { SSN = "", FirstName = "", LastName = "", EmployeeRole = "", JobTitle = "", User_id = id });
        //}

        // GET: api/Employee
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
        [Route("api/Employee/GetEmployees/{User_id:int}/{SSN:string}")]
        [HttpGet]
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


        // POST: api/Employee
        public void CreateEmployee(Employee value)
        {
            employee.Add(value);
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
    }
}
