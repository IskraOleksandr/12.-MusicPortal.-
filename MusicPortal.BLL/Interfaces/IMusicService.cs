using MusicPortal.BLL.DTO;

namespace MusicPortal.BLL.Interfaces
{
    public interface IMusicService
    {
         Task AddSong(MusicDTO songDto);
        Task<MusicDTO> GetSong(int id);
        Task<IEnumerable<MusicDTO>> GetAllSongs();
        Task<IEnumerable<MusicDTO>> FindSongs(string str);
        Task AddSongToArtist(int id, MusicDTO songDto);
        Task DeleteSong(int id);
        Task UpdateSong(MusicDTO songDto);
    }
}
