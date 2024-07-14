using Entities.Dto;
using Entities.Models;

namespace Business.Abstract;

public interface IUserService
{
    Task<ResponseModel<UserDto>> Register(UserForRegisterDto model);
    Task<ResponseModel<UserDto>> UpdateUser(UserDto user);
    Task<ResponseModel<UserDto>> GetUserById(int id);
    Task<ResponseModel<UserDto>> Login(UserForLoginDto model);
}