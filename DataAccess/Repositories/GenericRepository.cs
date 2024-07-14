using DataAccess.Concrete;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;
public class GenericRepository<T> : IRepositoryBase<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> Add(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<T> Get(int id)
    {
        var result = await _context.FindAsync<T>(id);

        return result;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var result = await _context.Set<T>().ToListAsync();
        return result;
    }

    public async Task<T> Update(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();

        return entity;
    }
}
