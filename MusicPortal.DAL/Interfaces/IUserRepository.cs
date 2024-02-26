using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface IUserRepository : ISetGetRepository<User>
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUser(string name);
        Task<User> GetEmail(string email);
        Task<List<User>> GetUsers(string n);
        Task Update(int id, int l);
        Task Update(User u);
        Task Delete(int id);
        Task<bool> CheckEmail(string s);
        Task<bool> GetLogins(string s);
    }
}
