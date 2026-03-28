using System.ComponentModel.DataAnnotations;
using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace AppManager.Infrastructure.Services;

public class FeatureService : IFeatureService
{
    private readonly AppManagerDbContext _context;
    private readonly DbSet<Feature> _features;

    public FeatureService(AppManagerDbContext context)
    {
        _context = context;
        _features = context.Set<Feature>();
    }

    public void Create(Feature entity)
    {
        
        CheckDuplicates(entity);
        _features.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Feature entity)
    {
        CheckDuplicates(entity);
        _context.SaveChanges();
    }

    private void CheckDuplicates(Feature feature)
    {
        if (feature.Id > 0 && _features.Any(x => x.Id != feature.Id && x.Key == feature.Key) || _features.Any(x =>  x.Key == feature.Key))
        {
            throw new ValidationException($"Ya existe un feature con key: {feature.Key}"); 
        }
    }

    public void Delete(Feature entity)
    {
        _features.Remove(entity);
        _context.SaveChanges();
    }

    public Feature? GetById(int id)
    {
        return _features.Include(x => x.Permissions).FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<FeatureWPermissionsCount> GetAll(string query, bool? isActive)
    {
        var features = _features
            .Where(x =>
                (isActive == null || x.IsActive == isActive) &&
                (
                    string.IsNullOrWhiteSpace(query)
                    || EF.Functions.Like(x.Name.ToUpper(), $"%{query.ToUpper()}%")
                    || EF.Functions.Like(x.Key.ToUpper(), $"%{query.ToUpper()}%")
                    || EF.Functions.Like(x.MenuIcon.ToUpper(), $"%{query.ToUpper()}%")
                    || EF.Functions.Like(x.MenuPath.ToUpper(), $"%{query.ToUpper()}%")
                )
            )
            .OrderBy(x => x.MenuOrder)
            .Select(x => new FeatureWPermissionsCount(x.Id,
                x.Name,
                x.Key,
                x.MenuLabel,
                x.MenuPath,
                x.MenuIcon,
                x.MenuOrder,
                x.ShowInMenu,
                x.ParentId,
                x.IsActive,
                x.Permissions.Count
            ));

        return features;
    }

    public IEnumerable<Feature> GetAll(string query)
    {
        var features = _features.Include(x => x.Permissions).Where(x =>
            string.IsNullOrWhiteSpace(query)
            || EF.Functions.Like(x.Name.ToUpper(), $"%{query.ToUpper()}%")
            || EF.Functions.Like(x.Key.ToUpper(), $"%{query.ToUpper()}%")
            || EF.Functions.Like(x.MenuIcon.ToUpper(), $"%{query.ToUpper()}%")
            || EF.Functions.Like(x.MenuPath.ToUpper(), $"%{query.ToUpper()}%")
        ).OrderBy(x => x.MenuOrder);

        return features;
    }
}