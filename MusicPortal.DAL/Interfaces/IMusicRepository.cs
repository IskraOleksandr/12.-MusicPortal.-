using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
    public interface IMusicRepository : IRepository<Music>, ISetGetRepository<Music>
    {
        Task<List<Music>> FindSongs(string str);
        Task AddSongToArtist(int id, Music s);
        void Update(Music s);

    }
}
