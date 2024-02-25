using MusicPortal.BLL.DTO;

namespace MusicPortal.BLL.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUser(string name);
        Task<UserDTO> GetEmail(string email);
        Task<IEnumerable<UserDTO>> GetUsers(string n);
        Task AddUser(UserDTO u);
        Task UpdateUser(int id, int l);
        Task<UserDTO> GetUser(int id);
        Task<bool> CheckEmail(string s);
        Task<bool> GetLogins(string s);
        Task CreateUser(UserDTO u);
        Task<bool> CheckPassword(UserDTO u, string p);
    }
}
