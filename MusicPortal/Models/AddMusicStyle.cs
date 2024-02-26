using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class AddMusicStyle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле стиля должно быть установлено.")]
        [Display(Name = "Название стиля")]
        public string? StyleName { get; set; }

        //ICollection<MusicDTO> Musics { get; set; }
    }
} 
