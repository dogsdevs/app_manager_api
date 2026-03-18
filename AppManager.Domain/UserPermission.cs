namespace AppManager.Domain;

public class UserPermission
{
    public int UserId { get; private set; }
    public int PermissionId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime ExpiresAt { get; set; }

    public Permission Permission { get; private set; }
    public User User { get; private set; }
    
    private UserPermission()
    {
    }

    public static UserPermission Create(int userId, int permissionId, bool isActive = true)
    {
        return new UserPermission
        {
            UserId = userId,
            PermissionId = permissionId,
            IsActive = isActive
        };
    }
}