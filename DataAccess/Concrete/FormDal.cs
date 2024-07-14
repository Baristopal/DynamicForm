using DataAccess.Abstract;
using DataAccess.Repositories;
using Entities.Concrete;

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
}
