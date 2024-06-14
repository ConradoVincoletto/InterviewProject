using Domain.DTOs;

namespace Domain.Interfaces.Services;

public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetUsers();
    Task<UserDTO> GetById(int? id);
    Task Add(UserDTO userDTO);
    Task Update(UserDTO userDTO);
    Task Remove(int? id);
}
