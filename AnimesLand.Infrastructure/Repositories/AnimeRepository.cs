using AnimesLand.Domain.Entities;
using AnimesLand.Domain.Interfaces;
using AnimesLand.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AnimesLand.Infrastructure.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AnimeDbContext _context;

        public AnimeRepository(AnimeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Anime>> GetAllAsync()
        {
            return await _context.Animes.ToListAsync();
        }

        public async Task<Anime?> GetByIdAsync(int id)
        {
            return await _context.Animes.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Anime>> SearchAsync(string? nome, string? diretor)
        {
            var query = _context.Animes.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(a => a.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(diretor))
                query = query.Where(a => a.Diretor.Contains(diretor));

            return await query.ToListAsync();
        }

        public async Task AddAsync(Anime anime)
        {
            await _context.Animes.AddAsync(anime);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Anime anime)
        {
            _context.Animes.Update(anime);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Anime anime)
        {
            _context.Animes.Remove(anime);
            await _context.SaveChangesAsync();
        }
    }
}
