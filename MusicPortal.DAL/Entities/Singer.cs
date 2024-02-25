using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Entities
{
    public class Singer
    {
        public int Id { get; set; }
        public string? SingerName { get; set; }

        public ICollection<Music> Musics { get; set; }
    }
}
