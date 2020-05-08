using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalQandAForum.ViewModels
{
    public class EditUserDetailsViewModel
    {
        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"(\w+@[a-zA-z_]+?\.+[a-zA-Z]{2,6})")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z ]*$")]
        public string Name { get; set; }
        [Required]
        public string Mobile { get; set; }
    }
}
