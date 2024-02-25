using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.DAL.Repositories
{
    public class MusicStyleRepository : IMusicStyleRepository
    {
        private Music_PortalContext db;

        public MusicStyleRepository(Music_PortalContext context)
        {
            this.db = context;
        }
        public async Task<MusicStyle> Get(int id)
        {
            return await db.MusicStyles.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<int> GetId(Music s)
        {
            MusicStyle a = s.MusicStyle;
            return a.Id;
        }
        public async Task<List<MusicStyle>> GetList()
        {
            return await db.MusicStyles.ToListAsync();
        }
        public async Task AddItem(MusicStyle s)
        {
            await db.AddAsync(s);
        }
        public async Task Delete(int id)
        {
            var f = await db.MusicStyles.FindAsync(id);
            if (f != null)
            {
                db.MusicStyles.Remove(f);

            }
        }
        public async Task Update(int id, string s)
        {
            var f = await db.MusicStyles.FindAsync(id);
            if (f != null)
            {
                f.StyleName = s;
                db.MusicStyles.Update(f);

            }
        }
    }
}
