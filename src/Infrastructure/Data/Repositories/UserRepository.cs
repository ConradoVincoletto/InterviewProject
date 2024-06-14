using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SqlDbContext _dbContext;

    public UserRepository(SqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> Create(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetById(int? id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        return user;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users.OrderBy(x => x.FirstName).ToListAsync();
    }

    public async Task<User> Remove(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> Update(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
}
