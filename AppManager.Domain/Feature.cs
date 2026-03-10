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

    public static Feature CreateFeature(string name, string key, int? parentId = null, bool isActive = true)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(key);

        return new Feature
        {
            Name = name,
            Key = key,
            ParentId = parentId,
            ShowInMenu = false,
            MenuLabel = null,
            MenuPath = null,
            MenuIcon = null,
            MenuOrder = -1,
            IsActive = isActive
        };
    }

    public static Feature CreateMenu(string name, string key,  string? label, string? path, string? icon,
        int order = 1, bool isActive = true)
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

    public void AddParentId(int parentId)
    {
        ParentId = parentId;
    }
}