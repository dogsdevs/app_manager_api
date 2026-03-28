namespace AppManager.Domain.Services;

public interface IFeatureService : BaseCrudService<Feature>
{
    IEnumerable<FeatureWPermissionsCount> GetAll(string query, bool? isActive);
}

public record FeatureWPermissionsCount(int Id, string Name, string Key, string MenuLabel, string MenuPath, string MenuIcon, int MenuOrder, bool ShowInMenu, int? ParentId, bool IsActive, int PermissionsCount);
