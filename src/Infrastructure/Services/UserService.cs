using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Add(UserDTO userDTO)
    {
        var user = _mapper.Map<User>(userDTO);
        await _repository.Create(user);
        userDTO.UserId = user.Id;
    }

    public async Task<UserDTO> GetById(int? id)
    {
        var user = await _repository.GetById(id);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<IEnumerable<UserDTO>> GetUsers()
    {
        var users = await _repository.GetAllUsers();
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task Remove(int? id)
    {
        var user = _repository.GetById(id).Result;
        await _repository.Remove(user);
    }

    public async Task Update(UserDTO userDTO)
    {
        var user = _mapper.Map<User>(userDTO);
        await _repository.Update(user);
    }
}
