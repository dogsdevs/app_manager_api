using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace AppManager.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly AppManagerDbContext _context;
    private readonly DbSet<Role> _roles;

    public RoleService(AppManagerDbContext context)
    {
        _context = context;
        _roles = _context.Set<Role>();
    }

    public void Create(Role entity)
    {
        _roles.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Role entity)
    {
        var role = GetById(entity.Id);
        if (role == null) return;
        
        _roles.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Role entity)
    {
        var role = GetById(entity.Id);
        if (role == null) return;
        
        _roles.Remove(entity);
        _context.SaveChanges();
    }

    public Role? GetById(int id)
    {
        return _roles.Find(id);
    }

    public IEnumerable<Role> GetAll(string query)
    {
        var roles = _roles
            .Where(x =>
                string.IsNullOrWhiteSpace(query)
                || EF.Functions.Like(x.Name.ToUpper(), $"%{query.ToUpper()}%")
            );
        return roles;
    }
    
}