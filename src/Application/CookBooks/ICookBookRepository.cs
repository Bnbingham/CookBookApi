namespace CookBookApi.Application.CookBooks;

using Entities;

public interface ICookBookRepository
{
    public Task<List<CookBook>> GetCookBooks(CancellationToken cancellationToken);
    public Task<CookBook> GetCookBookById(Guid id, CancellationToken cancellationToken);
    public Task<bool> CookBookExists(Guid id, CancellationToken cancellationToken);
}
