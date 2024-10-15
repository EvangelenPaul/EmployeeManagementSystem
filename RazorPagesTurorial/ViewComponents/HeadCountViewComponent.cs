using Microsoft.AspNetCore.Mvc;
using RazorPagesTurorial.Models;
using RazorPagesTurorial.Services;

namespace RazorPagesTurorial.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository employeeRepository;
        public HeadCountViewComponent(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IViewComponentResult Invoke(Dept? departmentName = null)
        {
            var result = employeeRepository.EmployeeCountByDept(departmentName);
            return View(result);
        }
    }
}
