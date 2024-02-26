using System.ComponentModel.DataAnnotations;
using MusicPortal.BLL.DTO;

namespace MusicPortal.Models
{
    public class AddSinger
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле имя исполнителя должно быть установлено.")]
        [Display(Name = "Имя исполнителя")]
        public string? SingerName { get; set; }

        //ICollection<MusicDTO> Musics { get; set; }
    }
}
