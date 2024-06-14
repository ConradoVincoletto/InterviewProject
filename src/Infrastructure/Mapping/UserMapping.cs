using AutoMapper;
using Domain.Entities;
using Domain.DTOs;

namespace Infrastructure.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDTO>().ReverseMap();
    }
}
