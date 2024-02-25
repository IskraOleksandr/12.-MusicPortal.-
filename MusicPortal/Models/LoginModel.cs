using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class LoginModel
    {
        [Required]
       // [Display(Name = "login: ")]
        [Display(Name = "loginN", ResourceType = typeof(Resources.Resource))]
        public string? Login { get; set; }

        [Required]
        // [Display(Name = "password: ")]
        [Display(Name = "password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        public string? Password { get; set; }   
    }
}
