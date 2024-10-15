using RazorPagesTurorial.Models;

namespace RazorPagesTurorial.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        Employee UpdateEmployee(Employee updatedEmployee);
        Employee AddEmployee(Employee newEmployee);
        Employee Delete(int id);
        IEnumerable<DeptHeadCount> EmployeeCountByDept(Dept? dept);
        IEnumerable<Employee> Search(string searchTerm);

    }
}
