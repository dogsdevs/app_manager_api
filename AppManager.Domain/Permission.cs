namespace AppManager.Domain;

public class Permission
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string GuardName { get; private set; }
    public int FeatureId { get; private set; }
    public string Action { get; private set; }

    public Feature Feature { get; private set; }

    public HashSet<RolePermission> Roles { get; private set; } = [];
    public HashSet<UserPermission> Permissions { get; private set; } = [];
    public HashSet<User> Users { get; private set; } = [];


    private Permission()
    {
    }

    public static Permission Create(int featureId, string action)
    {
        return new Permission
        {
            FeatureId = featureId,
            Action = action
        };
    }
}