using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesTurorial.Models;
using RazorPagesTurorial.Services;

namespace RazorPagesTurorial.Pages.Employees
{
    public class EditModel : PageModel
    {
        private IEmployeeRepository EmployeeRepository { get; set; }
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        public IFormFile? Photo { get; set; }

        [BindProperty]
        public bool Notify {  get; set; }

        public string Message { get; set; }

        public EditModel(IEmployeeRepository employeeRepository,
                         IWebHostEnvironment webHostEnvironment)
        {
            this.EmployeeRepository = employeeRepository;
            this.WebHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                Employee = EmployeeRepository.GetEmployee(id.Value);
            }

            else
            {
                Employee = new Employee();
            }

            if (Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Employee.Photopath != null)
                    {
                        string filePath = Path.Combine(WebHostEnvironment.WebRootPath, "images", Employee.Photopath);
                        System.IO.File.Delete(filePath);
                    }

                    Employee.Photopath = ProcessUploadedFile();
                }

                if (Employee.Id > 0)
                {
                    Employee = EmployeeRepository.UpdateEmployee(Employee);
                }
                else
                {
                    Employee = EmployeeRepository.AddEmployee(Employee);
                }
                
                return RedirectToPage("/Employees/Index");
            }

            return Page();
        }

        public IActionResult OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
            {
                Message = "Thank you for turning on notifications";
            }
            else
            {
                Message = "You have turned off email notifictaions";
            }
            TempData["message"] = Message;

            return RedirectToPage("Details", new { id = id });
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadFolder = Path.Combine(WebHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + " " + Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }
    }
}
