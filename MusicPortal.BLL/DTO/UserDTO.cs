using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Login { get; set; }
        public string? Password { get; set; }
        public int? Level { get; set; }
        public string email { get; set; }
        public string? Salt { get; set; }
    }
}
