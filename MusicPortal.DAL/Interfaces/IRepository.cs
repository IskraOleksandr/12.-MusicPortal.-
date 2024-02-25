using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetList();
        Task Delete(int id);
    }
}
