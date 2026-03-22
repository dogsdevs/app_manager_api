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
        _features.Add(entity);
        _context.SaveChanges();
    }

    public void Update(Feature entity)
    {
        _features.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(Feature entity)
    {
        _features.Remove(entity);
        _context.SaveChanges();
    }

    public Feature? GetById(int id)
    {
        return _features.Find(id);
    }

    public IEnumerable<Feature> GetAll(string query)
    {
        var features = _features.Where(x =>
            string.IsNullOrWhiteSpace(query)
            || EF.Functions.Like(x.Name.ToUpper(), $"%{query.ToUpper()}%")
            || EF.Functions.Like(x.Key.ToUpper(), $"%{query.ToUpper()}%")
            || EF.Functions.Like(x.MenuIcon.ToUpper(), $"%{query.ToUpper()}%")
            || EF.Functions.Like(x.MenuPath.ToUpper(), $"%{query.ToUpper()}%")
        ).OrderBy(x => x.MenuOrder);

        return features;
    }
}