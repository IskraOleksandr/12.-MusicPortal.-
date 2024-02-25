using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using AutoMapper;
using System.Numerics;
using System;

namespace MusicPortal.BLL.Services
{
    public class SingerService: ISingerService
    {
        IUnitOfWork Database { get; set; }

        public SingerService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task AddArtist(SingerDTO artistDto)
        {
            var a = new Singer
            {
                Id = artistDto.Id,
                SingerName = artistDto.SingerName
            };
            await Database.Artists.AddItem(a);
            await Database.Save();
        }
        public async Task<SingerDTO> GetArtist(int id)
        {
            var a = await Database.Artists.Get(id);
            if (a == null)
                throw new ValidationException("Wrong artist!", "");
            return new SingerDTO
            {
                Id = a.Id,
                SingerName = a.SingerName
            };
        }
        public async Task<IEnumerable<SingerDTO>> GetAllArtists()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Singer, SingerDTO>() );
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Singer>, IEnumerable<SingerDTO>>(await Database.Artists.GetList());
        }
        public async Task<int> GetArtistId(MusicDTO song)
        {
            Music s = await Database.Songs.Get(song.Id);
            return await Database.Artists.GetId(s);
        }
        public async Task DeleteArtist(int id)
        {
            await Database.Artists.Delete(id);
            await Database.Save();
        }
        public async Task UpdateArtist(int id,string n)
        {        
           await Database.Artists.Update(id, n);
            await Database.Save();
        }

    }
}
