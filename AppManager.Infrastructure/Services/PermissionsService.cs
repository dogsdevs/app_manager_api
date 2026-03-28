using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace AppManager.Infrastructure.Services;

public class PermissionsService(AppManagerDbContext context) : IPermissionsService
{
    private readonly AppManagerDbContext _context = context;
    private readonly DbSet<Permission> _permissions = context.Set<Permission>();

    public void DeleteByFeatureId(int featureId)
    {
        var permissions = _permissions.Where(p => p.FeatureId == featureId).ToList();
        _permissions.RemoveRange(permissions);
        _context.SaveChanges();
    }
}