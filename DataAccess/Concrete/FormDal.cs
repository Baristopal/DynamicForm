using DataAccess.Abstract;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;
public class FormDal : GenericRepository<Form>, IFormDal
{
    private readonly AppDbContext _context;
    public FormDal(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Field> AddField(Field field)
    {
        await _context.Field.AddAsync(field);
        await _context.SaveChangesAsync();
        return field;
    }

    public async Task<List<Form>> GetAllFormWithFields()
    {
        var result = await _context.Forms.Include(x => x.Fields).ToListAsync();
        return result;
    }

    public async Task<Form> GetFormWithFields(int id)
    {
        var result = await _context.Forms?.Include(x => x.Fields)?.FirstOrDefaultAsync(x => x.Id == id)!;
        return result!;
    }

}
