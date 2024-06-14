using Domain.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetById(int? id);
    Task<User> Create(User user);
    Task<User> Update(User user);
    Task<User> Remove(User user);
    Task<bool> Exists(Expression<Func<User, bool>> predicate);
}
