using RazorPagesTurorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPagesTurorial.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        public List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Mary", Department = Dept.HR, Email = "mary@gmail.com", Photopath = "mary.png" },
                new Employee() { Id = 2, Name = "John", Department = Dept.IT, Email = "john@gmail.com", Photopath = "john.png" },
                new Employee() { Id = 3, Name = "Sara", Department = Dept.IT, Email = "sara@gmail.com", Photopath = "sara.png" },
                new Employee() { Id = 4, Name = "David", Department = Dept.Payroll, Email = "david@gmail.com" }
            };
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee AddEmployee(Employee newEmployee)
        {
            newEmployee.Id = _employeeList.Max(x => x.Id) + 1;
            _employeeList.Add(newEmployee);
            return newEmployee;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == updatedEmployee.Id);
            if (employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Department = updatedEmployee.Department;
                employee.Email = updatedEmployee.Email;
                employee.Photopath = updatedEmployee.Photopath;
            }
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employeeToDelete = _employeeList.FirstOrDefault(x => x.Id == id);
            if (employeeToDelete != null)
            {
                _employeeList.Remove(employeeToDelete);
            }
            return employeeToDelete;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = _employeeList;

            if(dept.HasValue)
            {
                query = query.Where(a => a.Department == dept.Value);
            }
            
            return query.GroupBy(e => e.Department).Select(g => new DeptHeadCount()
            {
                Department = g.Key,
                Count = g.Count()
            }).ToList();
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return _employeeList; 
            }
             
            return _employeeList.Where(e=> e.Name.Contains(searchTerm) ||
                                           e.Email.Contains(searchTerm) );

        }
    }
}
