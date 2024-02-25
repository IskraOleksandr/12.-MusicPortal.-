using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
    public interface ISetGetRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task AddItem(T s);
    }
}
