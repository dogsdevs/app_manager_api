using System.Net;

namespace AppManager.Domain;

public class Tenant
{

    private Tenant()
    {
        
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    
    public string NormalizedNameAndSlug { get; private set; }

    public static Tenant Create(string Name, string Slug)
    {
        return new Tenant()
        {
            Name = Name,
            Slug = Slug,
        };
    }
    
    public static Tenant Create(int id, string Name, string Slug)
    {
        return new Tenant()
        {
            Id = id,
            Name = Name,
            Slug = Slug,
        };
    }

    public void Update(string name, string slug)
    {
        this.Name = name.Trim(); 
        this.Slug = slug.Trim();
    }
    
 
    public HashSet<User> Users { get; private set; } = [];
    public HashSet<Role> Roles { get; private set; } = [];
    public HashSet<AuditLog> AuditLogs { get; private set; } = [];
}