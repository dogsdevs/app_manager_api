namespace AppManager.Domain;

public class User
{
    public int Id { get; private set; }
    public string IdentityKey { get; private set; }
    public string? Email { get; set; }
    public int TenantId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; private set; }

    public Tenant Tenant { get; private set; }
    public HashSet<Role> Roles { get; private set; }
    public HashSet<Permission> Permissions { get; private set; } = [];
    
    private User()
    {
    }

    public static User Create(string identityKey, string email)
    {
        ArgumentNullException.ThrowIfNull(identityKey);

        return new User
        {
            IdentityKey = identityKey,
            Email = string.IsNullOrEmpty(email) ? null : email,
            CreatedAt = DateTime.UtcNow
        };
    }
}