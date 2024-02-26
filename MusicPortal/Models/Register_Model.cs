using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class Register_Model
    {
        [Required(ErrorMessage = "Поле имени должно быть установлено.")]
        [Display(Name = "Имя:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 25 символов")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Поле фамилия должно быть установлено.")]
        [Display(Name = "Фамилия:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 25 символов")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Поле логин должно быть установлено.")]
        [Display(Name = "Логин:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 25 символов")]
        public string? Login { get; set; }


        [Required(ErrorMessage = "Поле email должно быть установлено.")]
        [Display(Name = "Email:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 25 символов")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле пароль должно быть установлено.")]
        [Display(Name = "Пароль:")]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина пароля должна быть от 3 до 20 символов")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Поле подтверждения пароля является обязательным.")]
        [Display(Name = "Повторить пароль:")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }

        public int Level { get; set; }
    }
}
