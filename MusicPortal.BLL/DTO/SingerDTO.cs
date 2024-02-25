using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.DTO
{
    public class SingerDTO
    {
        public int Id { get; set; }
        [Required]
        public string SingerName { get; set; }
    }
}
