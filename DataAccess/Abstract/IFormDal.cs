using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IFormDal : IRepositoryBase<Form>
{
    Task<Field> AddField(Field field);
    Task<List<Form>> GetAllFormWithFields();
    Task<Form> GetFormWithFields(int id);
}