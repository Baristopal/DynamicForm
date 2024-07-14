using AutoMapper;
using Business.Abstract;
using Business.Helpers;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Business.Concrete;
public class UserManager : IUserService
{
    private readonly IUserDal _userDal;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserManager(IUserDal userDal, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userDal = userDal;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResponseModel<UserDto>> Register(UserForRegisterDto model)
    {
        try
        {
            var isExistUser = await _userDal.GetUserByEmail(model.Email);
            if (isExistUser is not null)
                return new ResponseModel<UserDto>(false, "User already exists");

            HashingHelper.CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);

            _ = int.TryParse(_httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int createdBy);

            var user = new User
            {
                Email = model.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedBy = createdBy,
                UserName = model.UserName,
            };

            var result = await _userDal.Add(user);
            var userDto = _mapper.Map<UserDto>(result);

            return new ResponseModel<UserDto>(userDto, true);

        }
        catch (Exception ex)
        {
            return new ResponseModel<UserDto>(false, ex.Message);
        }

    }

    public async Task<ResponseModel<UserDto>> Login(UserForLoginDto model)
    {
        try
        {
            var user = await _userDal.IsExistUserByUserName(model.UserName);
            if (user is null)
                return new ResponseModel<UserDto>(false, "User not found");

            if (!HashingHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return new ResponseModel<UserDto>(false, "Password is wrong");

            var userDto = _mapper.Map<UserDto>(user);
            return new ResponseModel<UserDto>(userDto, true);
        }
        catch (Exception ex)
        {
            return new ResponseModel<UserDto>(false, ex.Message);
        }
    }


    public async Task<ResponseModel<UserDto>> UpdateUser(UserDto user)
    {
        try
        {
            user.UpdatedAt = DateTime.UtcNow;
            var userEntity = _mapper.Map<User>(user);
            var result = await _userDal.Update(userEntity);
            return new ResponseModel<UserDto>(user, true);
        }
        catch (Exception ex)
        {
            return new ResponseModel<UserDto>(false, ex.Message);
        }
    }

    public async Task<ResponseModel<UserDto>> GetUserById(int id)
    {
        try
        {
            var result = await _userDal.Get(id);
            var userEntity = _mapper.Map<UserDto>(result);
            return new ResponseModel<UserDto>(userEntity, true);
        }
        catch (Exception ex)
        {
            return new ResponseModel<UserDto>(false, ex.Message);
        }
    }
}
