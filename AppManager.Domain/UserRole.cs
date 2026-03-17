namespace AppManager.Domain;

public class UserRole
{
    public int RoleId { get; private set; }
    public int UserId { get; private set; }
    public DateTime AssignedAt { get; set; }
}