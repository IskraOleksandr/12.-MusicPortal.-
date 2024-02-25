using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Entities
{
    public class Music
    {
        public int Id { get; set; }

        public string? Video_Name { get; set; }

        public string? Album { get; set; }

        public string? Year { get; set; }

        public string? Video_URL { get; set; }

        public DateTime VideoDate { get; set; }

        public virtual MusicStyle MusicStyle { get; set; }

        public virtual User User { get; set; }

        public virtual Singer Singer { get; set; }

      
        public int MusicStyleId { get; set; }

        public int UserId { get; set; }

        public int SingerId { get; set; }
    }
}
