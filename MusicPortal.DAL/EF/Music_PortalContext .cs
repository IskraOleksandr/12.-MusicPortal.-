using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.EF
{
    public class Music_PortalContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<MusicStyle> MusicStyles { get; set; }
        public DbSet<Singer> Singers { get; set; }

        public Music_PortalContext(DbContextOptions<Music_PortalContext> options)
            : base(options)
        {

            if (Database.EnsureCreated())
            {
                Users.Add(new User { FirstName = "Admin", LastName = "Admin", Login = "Admin", Email = "admin@gmail.com", Password = "0B2FFE4FAE90F11F26F4223C2FDC95BB", Salt = "3CFD6F6D6ECA8B1F7037082D46788621", Level = 2 });
                SaveChanges();
            }
        }
    }
}
