using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ISingerRepository Artists { get; }
        IMusicStyleRepository Styles { get; }
        IMusicRepository Songs { get; }
        IUserRepository Users { get; }
        Task Save();
    }
}
