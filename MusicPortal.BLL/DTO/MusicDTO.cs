﻿using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.DTO
{
    public class MusicDTO
    {
        public int Id { get; set; }
        public string Video_Name { get; set; }
        public string? music_style { get; set; }
        public int? music_styleId { get; set; }
        public string? Singer { get; set; }
        public int? singerId { get; set; }
        public DateTime VideoDate { get; set; }
        public string? Album { get; set; }
        public string Video_URL { get; set; }
        public string Year { get; set; }
        public string? User { get; set; }
        public int? userId { get; set; }
    }
}
