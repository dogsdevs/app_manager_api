namespace AppManager.Domain;

public class AuditLog
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Resource { get; set; }
    public string TenantId { get; set; }
    public string Action { get; set; }
    public string IpAddress { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Changes { get; set; }
}