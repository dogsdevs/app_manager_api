namespace AppManager.Domain;

public class Role
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string GuardName { get; private set; }
    public int TenantId { get; private set; }

    public Tenant Tenant { get; private set; }
    public HashSet<RolePermission> Permissions { get; private set; } = [];
    public HashSet<User> Users { get; private set; } = [];


    private Role()
    {
    }

    public static Role Create(string roleName, string description, string guardName, int tenantId)
    {
        ArgumentNullException.ThrowIfNull(roleName);

        return new Role
        {
            Name = roleName,
            Description = description,
            GuardName = guardName,  
            TenantId = tenantId,
        };
    }

    public void AddPermission(Permission permission)
    {
        if (Permissions.Any(x => x.PermissionId == permission.Id)) return;

        Permissions.Add(RolePermission.Create(Id, permission.Id));
    }

    public void RemovePermission(Permission permission)
    {
        var existingPermission = Permissions.FirstOrDefault(x => x.PermissionId == permission.Id);
        if (existingPermission != null) Permissions.Remove(existingPermission);
    }
}