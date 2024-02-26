using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;
using System.Security.Cryptography;

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
                string pass = "Qwerty-123";
                byte[] saltbuf = new byte[16];
                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);
                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();
                //Salt s = new();
                //s.salt = salt;
                string password = salt + pass;
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                Users.Add(new User { FirstName = "Admin", LastName = "Admin", Login = "admin", Password = hashedPassword, Email = "admin@gmail.com", Salt = salt, Level = 2 });
                //Admins.Add(a);
                SaveChanges();
                //Users.Add(new User { FirstName = "Admin", LastName = "Admin", Login = "Admin", Email = "admin@gmail.com", Password = "0B2FFE4FAE90F11F26F4223C2FDC95BB", Salt = "3CFD6F6D6ECA8B1F7037082D46788621", Level = 2 });
                //SaveChanges();
            }
        }
    }
}
