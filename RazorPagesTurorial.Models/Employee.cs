using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorPagesTurorial.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is missing"),
         MinLength(3, ErrorMessage = "Name should be atleast 3 characters")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9]+\.[a-zA-Z0-9-.]+$", ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }


        public string? Photopath { get; set; }

        [Required]
        public Dept Department { get; set; } 
    }
}
