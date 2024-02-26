using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Login_Model
    {
        [Required(ErrorMessage = "Поле Логин является обязательным.")]
        [Display(Name = "Логин:")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле Пароль является обязательным.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль:")]
        public string? Password { get; set; }
    }
}
