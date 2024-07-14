﻿using AutoMapper;
using Entities.Concrete;
using Entities.Dto;

namespace Business.AutoMapper;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>();
    }

}