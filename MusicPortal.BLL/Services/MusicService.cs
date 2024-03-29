﻿using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using AutoMapper;

namespace MusicPortal.BLL.Services
{
    public class MusicService: IMusicService
    {
        IUnitOfWork Database { get; set; }

        public MusicService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task AddSong(MusicDTO songDto)
        {
            Music s = await SongDTOToSong(songDto);
            await Database.Songs.AddItem(s);
            await Database.Save();
        }
        public async Task<MusicDTO> GetSong(int id)
        {
            var s = await Database.Songs.Get(id);
            if (s == null)
                throw new ValidationException("Wrong song!", "");
            return new MusicDTO
            {
                Id = s.Id,
                Video_Name = s.Video_Name,
                Video_URL = s.Video_URL,
                VideoDate = s.VideoDate,
                Album = s.Album,
                Singer = s.Singer.SingerName,
                singerId = s.Singer.Id,
                music_style = s.music_style.StyleName,
                music_styleId = s.music_style.Id,
                userId = s.UserId,
                User = s.User.Login,
                Year = s.Year,
            };
        }
        public async Task<IEnumerable<MusicDTO>> GetAllSongs()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Music, MusicDTO>()
            .ForMember("Singer", opt => opt.MapFrom(c => c.Singer.SingerName)).ForMember("music_style", opt => opt.MapFrom(c => c.music_style.StyleName))
            .ForMember("User", opt => opt.MapFrom(c => c.User.Login))
            );
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Music>, IEnumerable<MusicDTO>>(await Database.Songs.GetList());
        }
        public async Task<IEnumerable<MusicDTO>> FindSongs( string str)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Music, MusicDTO>()
             .ForMember("Singer", opt => opt.MapFrom(c => c.Singer.SingerName)).ForMember("music_style", opt => opt.MapFrom(c => c.music_style.StyleName)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Music>, IEnumerable<MusicDTO>>(await Database.Songs.FindSongs(str));
        }
        public async Task AddSongToArtist(int id, MusicDTO songDto)
        {
            Music s =await SongDTOToSong(songDto);
            await Database.Songs.AddSongToArtist(id, s);
            await Database.Save();
        }
        public async Task<Music> SongDTOToSong(MusicDTO songDto)
        {
            User us = await Database.Users.Get(songDto.userId.Value);
            Singer a = await Database.Artists.Get(songDto.singerId.Value);
            MusicStyle st = await Database.Styles.Get(songDto.music_styleId.Value);
            var s = new Music
            {
                Id = songDto.Id,
                Video_Name = songDto.Video_Name,
                Video_URL = songDto.Video_URL,
                VideoDate = songDto.VideoDate,
                Album = songDto.Album,
                Singer = a,
                SingerId = a.Id,
                music_style = st,
                MusicStyleId = st.Id,
                User = us,
                UserId = us.Id,
                Year = songDto.Year,
            };
            return s;
        }
        public async Task DeleteSong(int id)
        {
            await Database.Songs.Delete(id);
            await Database.Save();
        }
        public async Task UpdateSong(MusicDTO songDto)
        {
            Music s = await SongDTOToSong(songDto);          
              Database.Songs.Update(s);
             await Database.Save();
        }
    }
}
