using DataAccess.Abstract;
using DataAccess.Repositories;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete;
public class FormDal : GenericRepository<Form>, IFormDal
{
    public FormDal(AppDbContext context) : base(context)
    {
    }
}
