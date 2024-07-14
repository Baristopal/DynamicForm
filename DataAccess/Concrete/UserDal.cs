using DataAccess.Abstract;
using DataAccess.Repositories;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete;
public class UserDal : GenericRepository<User>, IUserDal
{
    private readonly AppDbContext _context;
    public UserDal(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var result = await _context?.Users?.FirstOrDefaultAsync(u => u.Email == email)!;
        return result!;
    }

    public async Task<User> IsExistUserByUserName(string userName)
    {
        var result = await _context?.Users?.FirstOrDefaultAsync(u => u.UserName == userName)!;
        return result!;
    }

}
