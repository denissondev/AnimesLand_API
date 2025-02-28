    using AnimesLand.Domain.Entities;

namespace AnimesLand.Domain.Interfaces
{
    public interface IAnimeRepository
    {
        Task<IEnumerable<Anime>> GetAllAsync();
        Task<Anime?> GetByIdAsync(int id);
        Task<IEnumerable<Anime>> SearchAsync(string? nome, string? diretor);
        Task AddAsync(Anime anime);
        Task UpdateAsync(Anime anime);
        Task DeleteAsync(Anime anime);
    }
}
