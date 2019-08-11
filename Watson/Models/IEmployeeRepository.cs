using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Watson.Models;


namespace Watson.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Employee_id);
        void Save(Employee employee);
    }
}
