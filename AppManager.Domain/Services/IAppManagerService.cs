namespace AppManager.Domain.Services;

public interface IAppManagerService
{
    // User Permissions and Menus
    Dictionary<string, bool> GetUserPermissions(string userId);
    Dictionary<string, Feature> GetUserMenus(string userId);

    // Users
    User RegisterUser(string identityKey, string email);
    // void AddUserPermission(string identityKey, int featureId, string action);

    // Features
    Feature AddFeature(Feature feature);

    void AddMenuChildren(int menuId, HashSet<int> children);
    
    HashSet<Feature> GetParentMenus(string identityKey);
}