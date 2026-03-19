using AppManager.Domain;
using AppManager.Domain.Exceptions;
using AppManager.Domain.Services;
using AppManager.Infrastructure.Exceptions;
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
        try
        {
            if (_tenants.Any(x => x.Name == entity.Name))
            {
                throw new DuplicateFieldException("name", "Ya existe un tenant con este nombre");
            } 
            
            if (_tenants.Any(x => x.Slug == entity.Slug))
            {
                throw new DuplicateFieldException("slug", "Ya existe un tenant con este slug");
            }

            _tenants.Add(entity);
            _context.SaveChanges();
        }
        catch (DbUpdateException ex) when (DbUniqueConstraintException.IsUniqueConstraintViolation(ex, out var conflictInfo))
        {
            var message = conflictInfo?.Field switch
            {
                nameof(Tenant.Name) => "Ya existe un tenant con este nombre.",
                nameof(Tenant.Slug) => "Ya existe un tenant con este slug.",
                _ => "Ya existe un registro con un valor duplicado."
            };

            throw new DuplicateFieldException(conflictInfo?.Field ?? "Unknown", message);

        }
    }

    public void Update(Tenant entity)
    {
        try
        {
            if (_tenants.Where(x => x.Id != entity.Id)
                .Any(x => x.Name == entity.Name))
            {
                throw new DuplicateFieldException("name", "Ya existe un tenant con este nombre");
            } 
            
            if (_tenants.Where(x => x.Id != entity.Id)
                .Any(x => x.Slug == entity.Slug))
            {
                throw new DuplicateFieldException("slug", "Ya existe un tenant con este slug");
            }
            
            _tenants.Update(entity);
            _context.SaveChanges();
        }
       catch (DbUpdateException ex) when (DbUniqueConstraintException.IsUniqueConstraintViolation(ex, out var conflictInfo))
        {
            var message = conflictInfo?.Field switch
            {
                nameof(Tenant.Name) => "Ya existe un tenant con este nombre.",
                nameof(Tenant.Slug) => "Ya existe un tenant con este slug.",
                _ => "Ya existe un registro con un valor duplicado."
            };

            throw new DuplicateFieldException(conflictInfo?.Field ?? "Unknown", message);
        }
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