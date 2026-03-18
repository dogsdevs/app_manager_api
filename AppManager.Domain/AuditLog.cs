namespace AppManager.Domain;

public class AuditLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Resource { get; set; }
    public int TenantId { get; set; }
    public string Action { get; set; }
    public string IpAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Changes { get; set; }
    
    public Tenant Tenant { get; private set; }
    public User User { get; private set; }
    
}