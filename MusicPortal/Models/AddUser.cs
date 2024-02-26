using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class AddUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле имени должно быть установлено.")]
        [Display(Name = "Имя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 25 символов")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Поле фамилии должно быть установлено.")]
        [Display(Name = "Фамилия")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 25 символов")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Поле логина должно быть установлено.")]
        [Display(Name = "Логин")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 25 символов")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Поле адреса должно быть установлено.")]
        [Display(Name = "Адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Remote(action: "CheckEmail", controller: "User", ErrorMessage = "Email уже используется")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Поле уровня доступа должно быть установлено.")]
        [Display(Name = "Уровень доступа")]
        [Range(0, 2, ErrorMessage = "Недопустимый уровень доступа")]
        public int Level { get; set; }
        
    }
}
