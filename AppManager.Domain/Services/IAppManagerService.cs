namespace AppManager.Domain.Services;

public interface IAppManagerService
{
    // User Permissions and Menus
    Dictionary<string, bool> GetUserPermissions(string userId);
    Dictionary<string, Feature> GetUserMenus(string userId);

    // Users
    User RegisterUser(string identityKey);
    void AddUserPermission(string identityKey, int featureId, string action);


    //Roles
    Role AddRole(Role role);
    void UpdateRole(Role entity);
    void DeleteRole(int roleId);
    Role? GetRoleById(int id);
    IEnumerable<Role> GetRoles(string query);


    // Features
    Feature AddFeature(Feature feature);

    void AddMenuChildren(int menuId, HashSet<int> children);
    
    HashSet<Feature> GetParentMenus(string identityKey);
}