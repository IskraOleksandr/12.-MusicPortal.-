using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System.Numerics;


namespace MusicPortal.DAL.Repositories
{
    public class MusicRepository : IMusicRepository
    {
        private Music_PortalContext db;

        public MusicRepository(Music_PortalContext context)
        {
            this.db = context;
        }
        public async Task<Music> Get(int id)
        {
            return await db.Musics.Include((p) => p.Singer).Include((p)=> p.User).Include((p) => p.music_style).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Music>> GetList()
        {
            return await db.Musics.Include((p) => p.Singer).Include((p) => p.User).Include((p) => p.music_style).ToListAsync();
        }
        public async Task<List<Music>> FindSongs(string str)
        {
            Singer a = await db.Singers.FirstOrDefaultAsync(m => m.SingerName == str);
            if (a == null)
                return await db.Musics.Where(son => son.Video_Name == str).Include((p) => p.Singer).Include((p) => p.music_style).ToListAsync();
            else
                return await db.Musics.Where(son => son.Singer == a).Include((p) => p.Singer).Include((p) => p.music_style).ToListAsync();
        }
        public async Task AddItem(Music s)
        {
            await db.AddAsync(s);
        }
        public async Task AddSongToArtist(int id, Music s)
        {

            var f = await db.Singers.FindAsync(id);
            Music s1 = await db.Musics.Where(son => son.Singer == f).FirstOrDefaultAsync(m => m.Video_Name == s.Video_Name);
            if (f != null && s1 != null)
            {
                f.Musics.Add(s1);
                db.Singers.Update(f);
            }
        }
        public async Task Delete(int id)
        {
            var f = await db.Musics.FindAsync(id);
            if (f != null)
            {
                db.Musics.Remove(f);
            }
        }
        public void Update(Music s)
        {
            try
            {
                db.Musics.Update(s);
                //db.Entry(s).State = EntityState.Modified;
               
            }
            catch { }
        }
    }
}
