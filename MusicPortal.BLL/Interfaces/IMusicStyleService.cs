using MusicPortal.BLL.DTO;


namespace MusicPortal.BLL.Interfaces
{
    public interface IMusicStyleService
    {
        Task AddStyle(MusicStyleDTO styleDto);
        Task<MusicStyleDTO> GetStyle(int id);
        Task<IEnumerable<MusicStyleDTO>> GetAllStyles();
        Task<int> GetStyleId(MusicDTO song);
        Task DeleteStyle(int id);
        Task UpdateStyle(int id, string n);
        Task UpdateMusicStyle(MusicStyleDTO a);
    }
}
