using MusicPortal.BLL.DTO;


namespace MusicPortal.BLL.Interfaces
{
    public interface ISingerService
    {
        Task AddArtist(SingerDTO artistDto);
        Task<SingerDTO> GetArtist(int id);
        Task<IEnumerable<SingerDTO>> GetAllArtists();
        Task<int> GetArtistId(MusicDTO song);
        Task DeleteArtist(int id);
        Task UpdateArtist(int id, string n, string p);
    }
}
