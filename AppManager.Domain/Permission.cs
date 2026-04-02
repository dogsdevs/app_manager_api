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

    public static Permission Create(int? id, int? featureId, string name, string guardName, string action,
        string description)
    {
        return new Permission
        {
            Id = id ?? 0,
            FeatureId = featureId ?? 0,
            Action = action,
            Name = name,
            GuardName = guardName,
            Description = description,
            Feature = null
        };
    }


    public void Update(string name, string description, string guardName, string action)
    {
        Name = name;
        Description = description;
        GuardName = guardName;
        Action = action;
    }
    
    
    protected bool Equals(Permission other)
    {
        return Id == other.Id;
    }
    
    public bool HasChanges(Permission other)
    {
        return Name != other.Name ||
               GuardName != other.GuardName ||
               Action != other.Action ||
               Description != other.Description;
    }

    public override bool Equals(object? obj)
    {
        return obj is Permission other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}