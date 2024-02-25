using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicPortal.DAL.Interfaces
{
    public interface ISingerRepository : ISetGetRepository<Singer>, IRepository<Singer>, IGetIdRepository
    {
        Task Update(int id, string s);
    }
}
