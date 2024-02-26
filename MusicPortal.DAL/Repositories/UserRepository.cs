using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories
{
    public class UserRepository: IUserRepository
    {
        private Music_PortalContext db;

        public UserRepository(Music_PortalContext context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }
        public async Task<User> GetUser(string login)
        {
            return await db.Users.FirstOrDefaultAsync(m => m.Login == login);
        }
        public async Task<User> GetEmail(string email)
        {
            return await db.Users.FirstOrDefaultAsync(m => m.Email == email);
        }
        public async Task<List<User>> GetUsers(string n)
        {
            return await db.Users.Where(user => user.Login != n).ToListAsync();
        }
        public async Task AddItem(User user)
        {
            await db.AddAsync(user);
        }
        public async Task Update(int id, int l)
        {
            var f = await db.Users.FindAsync(id);
            if (f != null)
            {
                f.Level = l;
                db.Users.Update(f);

            }
        }
        public async Task Update(User u)
        {
            var f = await db.Users.FindAsync(u.Id);
            if (f != null)
            {
                db.Users.Update(f);

            }
        }
        public async Task Delete(int id)
        {
            var c = await db.Users.FindAsync(id);
            if (c != null)
            {
                db.Users.Remove(c);

            }
        }
        public async Task<User> Get(int id)
        {
            return await db.Users.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<bool> CheckEmail(string s)
        {
            return await db.Users.AllAsync(u => u.Email == s);
        }
        public async Task<bool> GetLogins(string s)
        {
            return await db.Users.AllAsync(u => u.FirstName == s);

        }       
        public async Task<User> GetLogin(string s)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.FirstName == s);

        }
    }
}
