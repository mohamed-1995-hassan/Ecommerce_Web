using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Web.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
         ErrorMessage = "Password must have 1 upper case, 1 lower case, 1 number, 1 none alphanumeric, and at least 6 char")]
        public string Password { get; set; }
    }
}
