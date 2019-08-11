using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Watson.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Employee_id = 1, SSN = "001", FirstName = "VERNONtest", LastName = "PAPEtest" },
                new Employee() { Employee_id = 2, SSN = "002", FirstName = "VERNONtest2", LastName = "PAPEtest2" }
            };
        }

        public Employee GetEmployee(int Employee_id)
        {
            return this._employeeList.FirstOrDefault(e => e.Employee_id == Employee_id);
        }

        public void Save(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}


 