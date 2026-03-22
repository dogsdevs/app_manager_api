using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppManager.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RolesController(ILogger<AppManagerController> logger, IRoleService service, ITenantService tenantService)
    : ControllerBase
{

    [HttpGet]
    public IActionResult GetRoles([FromQuery] string query = "")
    {
        var result = service.GetAll(query).Select(x => new RoleDto(x.Id, x.Name, x.Description, x.GuardName, x.TenantId));
        return Ok(result);
    }

    [HttpGet("{roleId}")]
    public IActionResult GetRoleById(int roleId)
    {
        var result = service.GetById(roleId);
        if (result == null)
        {
            return NotFound();
        }
        var dto = new RoleDto(roleId, result.Name, result.Description, result.GuardName, result.TenantId);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult AddRole([FromBody] RequestRoleDto data)
    {
        var role = Role.Create(data.Name, data.Description, data.GuardName, data.TenantId);
        service.Create(role);
        
        return CreatedAtAction(nameof(GetRoleById), new { roleId = role.Id }, new RoleDto(role.Id, role.Name, role.Description, role.GuardName, role.TenantId));
    }

    [HttpPut("{roleId}")]
    public IActionResult UpdateRole(int roleId, [FromBody] RequestRoleDto data)
    {
        var role = service.GetById(roleId);
        if (role == null)
        {
            return NotFound();
        }
        
        var existsTenant = tenantService.GetById(data.TenantId);

        if (existsTenant == null)
        {
            return BadRequest("El tenant es invalido");
        }
        
        role.Update(roleId, data.Name, data.Description, data.GuardName, data.TenantId);
        
        service.Update(role);
        var roleDto = new RoleDto(role.Id, role.Name, role.Description, role.GuardName, role.TenantId);
        return Ok(roleDto);
    }

    [HttpDelete("{roleId}")]
    public IActionResult UpdateRole(int roleId)
    {
        var role = service.GetById(roleId);
        if (role == null)
        {
            return NotFound();
        }
        service.Delete(role);
        return NoContent();
    }
    
    
    // [HttpGet("/api/roles/{roleId}/permissions")]
    // public IActionResult GetRolePermissions(int roleId)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // [HttpPost("/api/roles/{roleId}/permissions")]
    // public IActionResult AddRolePermissions(int roleId, [FromBody] AddPermissionDto  data)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // [HttpDelete("/api/roles/{roleId}/permissions/{permissionId}")]
    // public IActionResult AddRolePermissions(int roleId, int permissionId)
    // {
    //     throw new NotImplementedException();
    // }

}

public record RequestRoleDto(string Name, string Description,string GuardName, int TenantId);


public record RoleDto(int Id, string Name, string Description, string GuardName, int TenantId);
