using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле имени должно быть установлено.")]
        [Display(Name = "Имя:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 25 символов")]
        public string? FirstName { get; set; }


        [Required(ErrorMessage = "Поле фамилия должно быть установлено.")]
        [Display(Name = "Фамилия:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина логина должна быть от 3 до 25 символов")]
        public string? LastName { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "LogRequired")]
        [Display(Name = "loginN", ResourceType = typeof(Resources.Resource))]
        [Remote("IsLoginIn", "Login", ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "loginused")]
        public string? Login { get; set; }



        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "PassRequired")]
        [Display(Name = "password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        public string? Password { get; set; }



        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "PassConRequired")]
        [Display(Name = "passwordConf", ResourceType = typeof(Resources.Resource))]
        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "passnoteq")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "unemail")]
        [Display(Name = "email ")]
        [Remote("IsEmailIn", "Login", ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "emailused")]
        public string? Email { get; set; }

        public int Level { get; set; }
    }
}
