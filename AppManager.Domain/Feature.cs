using System.ComponentModel.DataAnnotations;

namespace AppManager.Domain;

public class Feature
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Key { get; private set; }
    public int? ParentId { get; private set; }
    public bool ShowInMenu { get; private set; }
    public bool IsActive { get; private set; }
    public string? MenuLabel { get; private set; }
    public string? MenuPath { get; private set; }
    public string? MenuIcon { get; private set; }
    public int MenuOrder { get; private set; }

    public Feature? Parent { get; set; }
    public HashSet<Permission> Permissions { get; private set; } = [];

    private Feature()
    {
    }


    public static Feature CreateFeature(string name, string key, string menuLabel, string menuPath, string menuIcon,
        int menuOrder, bool showInMenu, int? parentId, bool isActive)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(key);

        return new Feature
        {
            Name = name,
            Key = key,
            ParentId = parentId == 0 ? null : parentId,
            Parent = null,
            ShowInMenu = showInMenu,
            MenuLabel = menuLabel,
            MenuPath = menuPath,
            MenuIcon = menuIcon,
            MenuOrder = menuOrder,
            IsActive = isActive
        };
        
    }

    public static Feature CreateFeature(string name, string key, int? parentId = null, bool isActive = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(key);

        return new Feature
        {
            Name = name,
            Key = key,
            ParentId = parentId == 0 ? null : parentId,
            Parent = null,
            ShowInMenu = false,
            MenuLabel = null,
            MenuPath = null,
            MenuIcon = null,
            MenuOrder = -1,
            IsActive = isActive
        };
    }

    public static Feature CreateMenu(string name, string key,  string? label, string? path, string? icon,
        int order = 1, bool isActive = false)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(key);

        return new Feature
        {
            Name = name,
            Key = key,
            ShowInMenu = true,
            MenuLabel = label,
            MenuPath = path,
            MenuIcon = icon,
            MenuOrder = order,
            IsActive = isActive
        };
    }

    public void Update(string name, string key, string menuLabel, string menuPath, string menuIcon,
        int menuOrder, bool showInMenu, int? parentId, bool isActive, Permission[] permissions)
    {
        Name = name;
        Key = key;
        ParentId = parentId == 0 ? null : parentId;
        Parent = null;
        ShowInMenu = showInMenu;
        MenuLabel = menuLabel;
        MenuPath = menuPath;
        MenuIcon = menuIcon;
        MenuOrder = menuOrder;
        IsActive = isActive;

        if (permissions.Length > 0)
        {
            ManagePermission(permissions);
        }
    }

    public void AddParentId(int parentId)
    {
        ParentId = parentId == 0 ? null : parentId;
    }

    private void CheckDuplicatePermission(Permission permission)
    {
        if (Permissions.Any(x => x.GuardName == permission.GuardName))
        {
            throw new ValidationException($"Ya existe un permiso del tipo: {permission.GuardName}"); 
        }
    }
    
    public void ManagePermission(Permission[] permissions)
    {
        var existingById = Permissions.ToDictionary(p => p.Id);
        var incomingIds = new HashSet<int>();

        foreach (var incoming in permissions)
        {
            if (incoming.Id == 0)
            {
                CheckDuplicatePermission(incoming);
                Permissions.Add(incoming);
                continue;
            }

            incomingIds.Add(incoming.Id);

            if (existingById.TryGetValue(incoming.Id, out var existing))
            {
                if (!existing.Equals(incoming))
                {
                    CheckDuplicatePermission(incoming);
                    existing.Update(incoming.Name,incoming.Description, incoming.GuardName, incoming.Action);
                }
            }
            else
            {
                Permissions.Add(incoming);
            }
        }

        Permissions.RemoveWhere(p => p.Id != 0 && !incomingIds.Contains(p.Id));
    }
}