using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class RegisterModel
    {
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
        public string? email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "AgeRequired")]
        [Display(Name = "age", ResourceType = typeof(Resources.Resource))]
        [Remote("CheckAge", "Login", ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "unage")]
        public string? age { get; set; }
    }
}
