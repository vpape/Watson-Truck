using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Watson.Models;

namespace Watson.Controllers
{
    public class AdminController : ApiController
    {


        List<Employee> employee = new List<Employee>();

        public AdminController()
        {
            employee.Add(new Employee { FirstName = "Vernon", LastName = "Pape", User_id = 1 });
            employee.Add(new Employee { FirstName = "Lynetta", LastName = "Richards", User_id = 2 });
            employee.Add(new Employee { FirstName = "LaNita", LastName = "Palmer", User_id = 3 });
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
            
        }
    }
}
