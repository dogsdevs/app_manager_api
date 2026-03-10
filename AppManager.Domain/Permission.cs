namespace AppManager.Domain;

public class Permission
{
    public int Id { get; private set; }
    public int FeatureId { get; private set; }
    public string Action { get; private set; }

    public Feature Feature { get; private set; }

    public HashSet<RolePermission> Roles { get; private set; } = [];
    public HashSet<UserPermission> Users { get; private set; } = [];


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