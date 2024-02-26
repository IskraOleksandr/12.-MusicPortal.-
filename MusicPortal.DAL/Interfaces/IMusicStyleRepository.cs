using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
    public interface IMusicStyleRepository : ISetGetRepository<MusicStyle>, IRepository<MusicStyle>, IGetIdRepository
    {
        Task Update(int id, string s);
        Task Update(MusicStyle u);
    }
}
