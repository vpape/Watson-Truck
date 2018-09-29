using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Watson.Models;

namespace Watson.Controllers
{
    /// <summary>
    /// This is where I give you all the information about my employees
    /// </summary>
    public class AdminController : ApiController
    {
        private WatsonTruckEntities db = new WatsonTruckEntities();

        List<Employee> employee = new List<Employee>();

        public AdminController()
        {
            employee.Add(new Employee { FirstName = "Vernon", LastName = "Pape", User_id = 1 });
            employee.Add(new Employee { FirstName = "Lynetta", LastName = "Richards", User_id = 2 });
            employee.Add(new Employee { FirstName = "LaNita", LastName = "Palmer", User_id = 3 });
        }

        /// <summary>
        /// Gets a list of the first names of all users
        /// </summary>
        /// <param name="userId">The unique identifier for this person</param>
        /// <param name="age">We want to know how old they are</param>
        /// <returns>Alist of first names</returns>
        [Route("api/Admin/GetFirstNames/{userId:int}/{age:int}")]
        [HttpGet]
        public List<string> GetFirstNames(int userId, int age)
        {
            List<string> output = new List<string>();

            foreach (var e in employee)
            {
                output.Add(e.FirstName);
            }

            return output;
        }

        // GET: api/Admin
        public List<Employee> Get()
        {
            return employee;
        }

        // GET: api/Admin/5
        public Employee Get(int id)
        {
            return employee.Where(x => x.User_id == id).FirstOrDefault();
        }

        // POST: api/Admin
        public void Post(Employee val)
        {
            employee.Add(val);
        }

        // DELETE: api/Admin/5
        public void Delete(int id)
        {
            //Employee employee = db.Employees.Find(id);
            //db.Employees.Remove(employee);
            //db.SaveChanges();

            //db.DeleteEmployeeAndDependents(id);

            //return RedirectToAction("Index");
        }
    }
}
