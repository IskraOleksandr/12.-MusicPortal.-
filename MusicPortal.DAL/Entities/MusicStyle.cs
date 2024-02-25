using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Entities
{
    public class MusicStyle
    {
        public int Id { get; set; }

        public string? StyleName { get; set; }

        ICollection<Music> Musics { get; set; }
    }
}
