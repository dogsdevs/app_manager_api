using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace AppManager.Infrastructure.Services;

public class AppManagerService : IAppManagerService
{
    private readonly AppManagerDbContext _context;

    public AppManagerService(AppManagerDbContext context)
    {
        _context = context;
    }

    public Dictionary<string, bool> GetUserPermissions(string userId)
    {
        throw new NotImplementedException();
        
        // var permissions = new Dictionary<string, bool>();
        // var user = GetPermissionsFull(userId);
        //
        // if (user == null) return permissions;
        //
        // foreach (var rolePermissions in user.Role.Permissions)
        // {
        //     var p = rolePermissions.Permission;
        //     var key = $"{p.Feature.Key}:{p.Action}";
        //
        //     permissions[key] = true;
        // }
        //
        // foreach (var userPermission in user.Permissions)
        // {
        //     var p = userPermission.Permission;
        //     var key = $"{p.Feature.Key}:{p.Action}";
        //
        //     permissions[key] = userPermission.IsActive;
        // }
        //
        // return permissions;
    }

    public Dictionary<string, Feature> GetUserMenus(string userId)
    {
        throw new NotImplementedException();
        // var permissions = new Dictionary<string, Feature>();
        // var user = GetPermissionsFull(userId, true);
        //
        // if (user == null) return permissions;
        //
        // foreach (var rolePermissions in user.Role.Permissions)
        // {
        //     var p = rolePermissions.Permission;
        //     var key = $"{p.Feature.Key}:{p.Action}";
        //
        //     permissions[key] = p.Feature;
        // }
        //
        // foreach (var userPermission in user.Permissions)
        // {
        //     var p = userPermission.Permission;
        //     var key = $"{p.Feature.Key}:{p.Action}";
        //
        //     if (permissions.TryGetValue(key, out _))
        //     {
        //         if (!p.Feature.IsActive) permissions.Remove(key);
        //     }
        //     else
        //     {
        //         permissions[key] = p.Feature;
        //     }
        // }
        //
        // return permissions;
    }
    
    public User RegisterUser(string identityKey, string email)
    {
        var user = User.Create(identityKey, email);
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public void AddUserPermission(string identityKey, int featureId, string action)
    {
        ArgumentNullException.ThrowIfNull(identityKey);

        var user = _context.Users.FirstOrDefault(x => x.IdentityKey == identityKey);

        if (user == null) throw new Exception($"User with identityKey {identityKey} not found");

        var feature = _context.Features.FirstOrDefault(x => x.Id == featureId);
        if (feature == null) throw new Exception($"Feature with id {featureId} not found");

        var permission = Permission.Create(feature.Id, action);
        var userPermission = UserPermission.Create(user.Id, permission.Id);

        _context.Permissions.Add(permission);
        _context.UserPermissions.Add(userPermission);
        _context.SaveChanges();
    }


    public Feature AddFeature(Feature feature)
    {
        var data = _context.Features.Add(feature);
        _context.SaveChanges();
        return feature;
    }

    public void AddMenuChildren(int menuId, HashSet<int> children)
    {
        var feature = _context.Features
            .FirstOrDefault(x =>
                x.Id == menuId
                && x.ParentId == null
                && x.ShowInMenu == true
            );

        if (feature == null) throw new Exception($"Invalid parent menu with id '{menuId}'.");

        var childMenus = _context.Features.Where(x =>
            x.ParentId == null
            && x.ShowInMenu == true
            && children.Contains(x.Id)
        ).ToList();

        foreach (var child in childMenus) child.AddParentId(menuId);

        _context.Features.UpdateRange(childMenus);
        _context.SaveChanges();
    }

    public HashSet<Feature> GetParentMenus(string identityKey)
    {
        var userMenus =  GetUserMenus(identityKey)
            .Where(x => x.Value.Parent== null && x.Value.IsActive);
        
        var menus = userMenus.Select(x => x.Value).ToHashSet();

        return menus;
    }



    private User? GetPermissionsFull(string userId, bool isMenu = false)
    {
        throw new NotImplementedException();
        
        // return _context.Users
        //     .Include(x => x.Role)
        //     .ThenInclude(x => x.Permissions
        //         .Where(s => !isMenu || s.Permission.Feature.ShowInMenu == isMenu))
        //     .ThenInclude(x => x.Permission)
        //     .ThenInclude(x => x.Feature)
        //     .Include(x => x.Permissions
        //         .Where(s => !isMenu || s.Permission.Feature.ShowInMenu == isMenu))
        //     .ThenInclude(x => x.Permission)
        //     .ThenInclude(x => x.Feature)
        //     .FirstOrDefault(x => x.IdentityKey == userId);
    }
}