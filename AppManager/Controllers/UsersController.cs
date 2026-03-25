using AppManager.Domain.Services;
using AppManager.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(ILogger<UsersController> logger, IUserService service,  ITenantService tenantService)
    : ControllerBase
{

    [HttpGet]
    public IActionResult Get(string query = "", bool? isActive = null)
    {
        var users = service.GetAll(query, isActive)
            .Select(x => new UserDto(x.Id, x.TenantId, x.IdentityKey, x.Email, x.IsActive));
        
        return Ok(users);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var user = service.GetById(id);

        if (user == null)
        {
            return NotFound();
        }
        
        var  userDto = new UserDto(user.Id, user.TenantId, user.IdentityKey, user.Email, user.IsActive);
        
        return Ok(userDto);
    }

    [HttpPost]
    public IActionResult Create([FromBody] RequestUserDto userDto)
    {
        if (service.GetAll(userDto.IdentityKey).Any())
        {
            return Conflict("El usuario ya existe");
        }
        
        var existsTenant = tenantService.GetById(userDto.TenantId);

        if (existsTenant == null)
        {
            return BadRequest("El tenant es invalido");
        }
        
        var user = AppManager.Domain.User.Create(userDto.TenantId, userDto.IdentityKey, userDto.Email);
        service.Create(user);
        
        var dto = new UserDto(user.Id, user.TenantId, user.IdentityKey, user.Email, user.IsActive);
        
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateUserDto userDto)
    {
        var user = service.GetById(id);

        if (user == null)
        {
            return NotFound();
        }
        
        var existsTenant = tenantService.GetById(userDto.TenantId);

        if (existsTenant == null)
        {
            return BadRequest("El tenant es invalido");
        }
        
        user.Update(userDto.TenantId, userDto.Email, userDto.IsActive);
        
        service.Update(user);
        var dto = new UserDto(user.Id, user.TenantId, user.IdentityKey, user.Email, user.IsActive);
        return Ok(dto);
    }


    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var user = service.GetById(id);

        if (user == null)
        {
            return NotFound();
        }
        
        service.Delete(user);
        return NoContent();
    }
    
}

public record UserDto (int Id, int TenantId, string IdentityKey, string Email, bool IsActive);
public record RequestUserDto (int TenantId, string IdentityKey, string Email);
public record UpdateUserDto (int TenantId, string Email, bool IsActive);
