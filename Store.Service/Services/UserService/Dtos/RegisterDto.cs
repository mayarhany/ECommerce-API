using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.UserService.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?= ^.{6, 10}$(?=.*\\d)(?=.*[a - z])(?=.*[A - Z])(?=.*[!@#$%^&amp;*()_+}{&qout;:;'?/&gt;.&lt;,])(?!=.*\\s).*$", 
            ErrorMessage = "Password Must Have 1 Uppercase, 1 Lowercase, 1 Number, 1 Non alphanumaric and at least 6 Characters")]
        public string Password { get; set; }
    }
}
