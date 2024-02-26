using MusicPortal.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class AddMusic
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле название должно быть установлено.")]
        [Display(Name = "Название:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина название должна быть от 3 до 25 символов")]
        public string? Video_Name { get; set; }

        [Required(ErrorMessage = "Поле альбом должно быть установлено.")]
        [Display(Name = "Альбом:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина альбома должна быть от 3 до 25 символов")]
        public string? Album { get; set; }

        [Required(ErrorMessage = "Поле года выпуска должно быть установлено.")]
        [Display(Name = "Год выпуска:")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Длина го должна быть от 3 до 25 символов")]
        public string? Year { get; set; }

        [Display(Name = "Видео:")]
        [Required(ErrorMessage = "Поле файла должно быть установлено.")]
        public string? Video_URL { get; set; }

        [Display(Name = "Дата публикации:")]
        public DateTime VideoDate { get; set; }

        [Required(ErrorMessage = "Поле стиля должно быть установлено.")]
        [Display(Name = "Стиль:")]
        public int MusicStyleId { get; set; }

        [Display(Name = "Разместил:")]
        public string? UserLogin { get; set; }

        [Display(Name = "Разместил:")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Поле исполнителя должно быть установлено.")]
        [Display(Name = "Исполнитель:")]
        public int SingerId { get; set; }
        public string? SingerName { get; set; }
        public int? SongId { get; set; }
    }
}