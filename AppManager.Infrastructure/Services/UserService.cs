using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace AppManager.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly AppManagerDbContext _context;
    private readonly DbSet<User> _users;

    public UserService(AppManagerDbContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public void Create(User entity)
    {
        _users.Add(entity);
        _context.SaveChanges();
    }

    public void Update(User entity)
    {
        _users.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(User entity)
    {
        _users.Remove(entity);
        _context.SaveChanges();
    }

    public User? GetById(int id)
    {
        return _users.Find(id);
    }

    public IEnumerable<User> GetAll(string query)
    {
        var users = _users.Where(x =>
            string.IsNullOrWhiteSpace(query)
            || EF.Functions.Like(x.IdentityKey, $"%{query}%")
            || EF.Functions.Like(x.Email.ToUpper(), $"%{query.ToUpper()}%")
        );

        return users;
    }
}