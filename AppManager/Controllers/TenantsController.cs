using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TenantsController(ILogger<AppManagerController> logger, ITenantService service)
    : ControllerBase
{
    [HttpGet()]
    public IActionResult GetTenants([FromQuery]string q = "")
    {
        var data = service.GetAll(q);
        var dto = data.Select(x => new TenantDto(x.Id, x.Name, x.Slug));

        return Ok(dto);
    }

    [HttpGet("{id}")]
    public IActionResult GetTenant(int id)
    {
        var data = service.GetById(id);

        if (data == null)
        {
            return NotFound();
        }

        var dto = new TenantDto(data.Id, data.Name, data.Slug);
        return Ok(dto);
    }
    
    [HttpPost]
    public IActionResult Create(TenantDtoRequest dtoRequest)
    {
        try
        {
            var tenant = Tenant.Create(dtoRequest.Name, dtoRequest.Slug);
            service.Create(tenant);
            return CreatedAtAction(nameof(GetTenant), new { id = tenant.Id }, new TenantDto(tenant.Id, tenant.Name, tenant.Slug));
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, TenantDtoRequest dtoRequest)
    {
        try
        {
            var currentTenant = service.GetById(id);
            if (currentTenant == null)
            {
                return NotFound();
            }
            
            currentTenant.Update(dtoRequest.Name, dtoRequest.Slug);
            
            service.Update(currentTenant);
            
            return Ok(new TenantDto(currentTenant.Id, currentTenant.Name, currentTenant.Slug));
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var tenant = service.GetById(id);
        if (tenant == null)
        {
            return NotFound();
        }
        
        service.Delete(tenant);
        return NoContent();
    }
}

public record TenantDto(int Id, string Name, string Slug);

public record TenantDtoRequest(string Name, string Slug);