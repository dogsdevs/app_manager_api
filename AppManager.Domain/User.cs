namespace AppManager.Domain;

public class User
{
    public int Id { get; private set; }
    public int? RoleId { get; private set; }
    public string IdentityKey { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Role Role { get; private set; }
    public HashSet<UserPermission> Permissions { get; private set; } = [];

    private User()
    {
    }

    public static User Create(string identityKey, Role? role = null)
    {
        ArgumentNullException.ThrowIfNull(identityKey);

        return new User
        {
            IdentityKey = identityKey,
            RoleId = role?.Id ?? null,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void AddRole(Role role)
    {
        RoleId = role.Id;
    }

    public void RemoveRole()
    {
        RoleId = null;
    }
}