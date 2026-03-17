using System.Net;

namespace AppManager.Domain;

public class Tenant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
}