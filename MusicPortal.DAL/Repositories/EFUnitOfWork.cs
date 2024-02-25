using MusicPortal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;
using System.Numerics;
using MusicPortal.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.DAL.Repositories
{
    public class EFUnitOfWork: IUnitOfWork
    {
        private Music_PortalContext db;
        private SingerRepository artistRepository;
        private MusicStyleRepository styleRepository;
        private MusicRepository songRepository;
        private UserRepository userRepository;

        public EFUnitOfWork(Music_PortalContext context)
        {
            db = context;
        }

        public ISingerRepository Artists
        {
            get
            {
                if (artistRepository == null)
                    artistRepository = new SingerRepository(db);
                return artistRepository;
            }
        }

        public IMusicStyleRepository Styles
        {
            get
            {
                if (styleRepository == null)
                   styleRepository = new MusicStyleRepository(db);
                return styleRepository;
            }
        }
        public IMusicRepository Songs
        {
            get
            {
                if (songRepository == null)
                    songRepository = new MusicRepository(db);
                return songRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }      
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
