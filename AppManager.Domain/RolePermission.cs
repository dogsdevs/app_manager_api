namespace AppManager.Domain;

public class RolePermission
{
    public int Id { get; private set; }
    public int RoleId { get; private set; }
    public int PermissionId { get; private set; }

    public Role Role { get; private set; }
    public Permission Permission { get; private set; }

    private RolePermission()
    {
    }

    public static RolePermission Create(int roleId, int permissionId)
    {
        return new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        };
    }
}