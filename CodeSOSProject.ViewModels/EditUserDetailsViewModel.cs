using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSOSProject.ViewModels
{
    public class EditUserDetailsViewModel
    {       
        public int UserID { get; set; }

        [Required]
        [RegularExpression(@"(\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,6})")]
        public string Email { get; set; }

        [RegularExpression(@"^[a-zA-Z ]*$")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Mobile { get; set; }
    }
}
