using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using AutoMapper;

namespace MusicPortal.BLL.Services
{
    public class MusicStyleService: IMusicStyleService
    {
        IUnitOfWork Database { get; set; }

        public MusicStyleService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task AddStyle(MusicStyleDTO styleDto)
        {
            var st = new MusicStyle
            {
                Id = styleDto.Id,
                StyleName = styleDto.StyleName,
            };
            await Database.Styles.AddItem(st);
            await Database.Save();
        }
        public async Task<MusicStyleDTO> GetStyle(int id)
        {
            var st = await Database.Styles.Get(id);
            if (st == null)
                throw new ValidationException("Wrong artist!", "");
            return new MusicStyleDTO
            {
                Id = st.Id,
                StyleName = st.StyleName,
            };
        }
        public async Task<IEnumerable<MusicStyleDTO>> GetAllStyles()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<MusicStyle, MusicStyleDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<MusicStyle>, IEnumerable<MusicStyleDTO>>(await Database.Styles.GetList());
        }
        public async Task<int> GetStyleId(MusicDTO song)
        {
            Music s = await Database.Songs.Get(song.Id);
            return await Database.Styles.GetId(s);
        }
        public async Task DeleteStyle(int id)
        {
            await Database.Styles.Delete(id);
            await Database.Save();
        }
        public async Task UpdateStyle(int id,string n)
        {
            await Database.Styles.Update(id,n);
            await Database.Save();
        }
        public async Task UpdateMusicStyle(MusicStyleDTO a)
        {
            MusicStyle u = await Database.Styles.Get(a.Id);
            u.Id = a.Id;
            u.StyleName = a.StyleName;
            //u.Musics = a.;
            //u.Level = a.Level.Value;
            //u.Email = a.email;
            //u.Login = a.Login;

            //if (u.Password != a.Password)
            //{

            //}
            await Database.Styles.Update(u);
            await Database.Save();
        }
    }
}
