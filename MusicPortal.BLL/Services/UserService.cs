using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortal.BLL.Services
{
    public class UserService: IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<UserDTO> GetUser(string name)
        {
            var u = await Database.Users.GetUser(name);
            if (u == null)
                return null;
            return UserToUserDTO(u);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
                var mapper = new Mapper(config);
                return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetAll());
            }
            catch { return null; }
        }

        public async Task<UserDTO> GetEmail(string email)
        {
            var u = await Database.Users.GetEmail(email);
            if (u == null)
                return null;
            return UserToUserDTO(u);
        }
        public async Task<IEnumerable<UserDTO>> GetUsers(string n)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.Users.GetUsers(n));
        }
        public async Task AddUser(UserDTO u)
        {
            byte[] saltbuf = new byte[16];
            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);
            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();
           
            string password = salt + u.Password;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                Id = u.Id,
                FirstName = u.First_Name,
                LastName = u.Last_Name,
                Login = u.Login,
                Password = hashedPassword,
                Level = u.Level.Value,
                Email = u.email,
                Salt = salt,
            };
            await Database.Users.AddItem(user);
            await Database.Save();
        }
        public async Task UpdateUser(int id, int l)
        {
            await Database.Users.Update(id, l);
            await Database.Save();
        }
        public async Task UpdateUser(UserDTO a)
        {
            User u = await Database.Users.Get(a.Id);
            u.FirstName = a.First_Name;
            u.LastName = a.Last_Name;
            u.Level = a.Level.Value;
            u.Email = a.email;
            u.Login = a.Login;

            if (u.Password != a.Password)
            {

            }
            await Database.Users.Update(u);
            await Database.Save();
        }
        public async Task DeleteUser(int id)
        {
            User user = await Database.Users.Get(id);
            await Database.Users.Delete(id);
            await Database.Save();
        }
        public async Task<UserDTO> GetUser(int id)
        {
            var u = await Database.Users.Get(id);
            if (u == null)
                throw new ValidationException("Wrong artist!", "");
            return   UserToUserDTO( u);           
        }
        public UserDTO UserToUserDTO(User u)
        {
            return new UserDTO
            {
                Id = u.Id,
                First_Name = u.FirstName,
                Last_Name = u.LastName,
                Login = u.Login,
                Password = u.Password,
                Level = u.Level,
                email = u.Email,
                Salt = u.Salt,
            };
        }
        public async Task<bool> CheckEmail(string s)
        {
            return await Database.Users.CheckEmail(s);
        }
        public async Task<bool> GetLogins(string s)
        {
            return await Database.Users.GetLogins(s);
        }
        public async Task CreateUser(UserDTO user)
        {           
                 string salt =await CreateSalt();
                  string password = salt + user.Password;
                  string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            User u = new()
            {
                FirstName = user.First_Name,
                LastName = user.Last_Name,
                Login = user.Login,
                Password = password,
                Level = user.Level.Value,
                Email = user.email,
                Salt = salt,
            };
            await Database.Users.AddItem(u);
            await Database.Save();
            
        }
        public async Task<bool> CheckPassword(UserDTO u,string p)
        {
            var user = new User
            {
                Id = u.Id,
               
                Login = u.Login,
                Password = u.Password,
                
            };
            User s = await Database.Users.Get(user.Id);
            string conf = s.Salt + p;
            if (BCrypt.Net.BCrypt.Verify(conf, user.Password))
                return true;
            else
                return false;
        }


        public async Task<string> CreateSalt()
        {
            byte[] saltbuf = new byte[16];
            RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(saltbuf);
            StringBuilder sb = new StringBuilder(16);
            for (int i = 0; i < 16; i++)
                sb.Append(string.Format("{0:X2}", saltbuf[i]));
            string salt = sb.ToString();
            return salt;
        }
    }
}
