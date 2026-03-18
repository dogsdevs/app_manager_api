using System.Net;

namespace AppManager.Domain;

public class Tenant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    
 
    public HashSet<User> Users { get; private set; } = [];
    public HashSet<Role> Roles { get; private set; } = [];
    public HashSet<AuditLog> AuditLogs { get; private set; } = [];
}