using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RazorPagesTurorial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPagesTurorial.Services
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        public SQLEmployeeRepository(AppDbContext context)
        {
            Context = context;
        }

        public AppDbContext Context { get; }

        public Employee AddEmployee(Employee newEmployee)
        {
            Context.Database.ExecuteSqlRaw("spInsertEmployee {0}, {1}, {2}, {3}",
                newEmployee.Name, newEmployee.Email, newEmployee.Photopath, newEmployee.Department);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            Employee employee = Context.Employee.Find(id);
            if (employee != null )
            {
                Context.Employee.Remove(employee);
                Context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept)
        {
            IEnumerable<Employee> query = Context.Employee;

            if (dept.HasValue)
            {
                query = query.Where(a => a.Department == dept.Value);
            }

            return query.GroupBy(e => e.Department).Select(g => new DeptHeadCount()
            {
                Department = g.Key,
                Count = g.Count()
            }).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return Context.Employee.FromSqlRaw<Employee>("SELECT * from Employee").ToList();
        }

        public Employee GetEmployee(int id)
        {
            SqlParameter parameter = new SqlParameter("@Id", id);
            
            return Context.Employee.FromSqlRaw<Employee>("spGetEmployeeById {0}", parameter).ToList().FirstOrDefault();
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return Context.Employee;
            }

            return Context.Employee.Where(e => e.Name.Contains(searchTerm) ||
                                           e.Email.Contains(searchTerm));

        }

        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            var employee = Context.Employee.Attach(updatedEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Context.SaveChanges();
            return updatedEmployee;
        }
    }
}
