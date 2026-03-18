using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace AppManager.Infrastructure.Services;

public class TenantService: ITenantService
{
    private readonly AppManagerDbContext _context;
    private readonly DbSet<Tenant> _tenants;

    public TenantService(AppManagerDbContext context)
    {
        _context = context;
        _tenants = _context.Set<Tenant>();
    }
    
    public void Create(Tenant entity)
    {
        var exists = _tenants.Any(x => x.Name == entity.Name || x.Slug == entity.Slug);
        if (exists)
        {
            throw  new Exception($"Tenant with name {entity.Name} or slug {entity.Slug} already exists");
        }
        _tenants.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Tenant entity)
    {
        var exists = _tenants.Any(x => x.Name == entity.Name || x.Slug == entity.Slug);
        if (exists)
        {
            throw  new Exception($"Tenant with name {entity.Name} or slug {entity.Slug} already exists");
        }
        
        _tenants.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Tenant entity)
    {
        _tenants.Remove(entity);
        _context.SaveChanges();
    }

    public Tenant? GetById(int id)
    {
        return _tenants.Find(id);
    }

    public IEnumerable<Tenant> GetAll(string query)
    {
        var tenants = _tenants
            .Where(x =>
                string.IsNullOrWhiteSpace(query)
                || EF.Functions.Like(x.Name, $"%{query}%")
                || EF.Functions.Like(x.Slug, $"%{query}%")
            );
        return tenants;
    }
}