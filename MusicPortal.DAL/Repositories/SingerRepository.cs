using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories
{
    public class SingerRepository : ISingerRepository
    {
        private Music_PortalContext db;

        public SingerRepository(Music_PortalContext context)
        {
            this.db = context;
        }
        public async Task<Singer> Get(int id)
        {
            return await db.Singers.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Singer>> GetList()
        {
            return await db.Singers.ToListAsync();
        }
        public async Task AddItem(Singer s)
        {
            await db.AddAsync(s);
        }
        public async Task Delete(int id)
        {
            var f = await db.Singers.FindAsync(id);
            if (f != null)
            {
                db.Singers.Remove(f);

            }
        }
        public async Task Update(int id, string s)
        {
            var f = await db.Singers.FindAsync(id);
            if (f != null)
            {
                f.SingerName = s;
                db.Singers.Update(f);

            }
        }
        public async Task<int> GetId(Music s)
        {
            Singer a = await db.Singers.FirstOrDefaultAsync(m => m == s.Singer);
            return a.Id;
        }
        public async Task Update(Singer u)
        {
            var f = await db.Singers.FindAsync(u.Id);
            if (f != null)
            {
                db.Singers.Update(f);

            }
        }
        
    }
}
