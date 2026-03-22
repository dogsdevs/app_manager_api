using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppManagerController(ILogger<AppManagerController> logger, IAppManagerService service)
    : ControllerBase
{
    private readonly ILogger<AppManagerController> _logger = logger;
    
    

    #region Users
    // [HttpGet("/api/users/{identityKey}/permissions")]
    // [Tags("Users")]
    // public IActionResult GetPermissions(string identityKey)
    // {
    //     var data = service.GetUserPermissions(identityKey);
    //     return Ok(data);
    // }
    //
    // [HttpGet("/api/users/{identityKey}/menus")]
    // [Tags("Users")]
    // public IActionResult GetMenu(string identityKey)
    // {
    //     var data = service.GetUserMenus(identityKey);
    //     return Ok(data);
    // }
    //
    // [HttpGet("/api/users/{identityKey}/menus/parents/")]
    // [Tags("Users")]
    // public IActionResult GetParentMenus([FromQuery] string identityKey)
    // {
    //     var menus = service.GetParentMenus(identityKey);
    //     return Ok(menus);
    // }

    // [HttpPost("/api/users")]
    // [Tags("Users")]
    // public IActionResult RegisterUser([FromBody] RegisterUserDto data)
    // {
    //     var result = service.RegisterUser(data.IdentityKey, data.Email);
    //     return Ok(result);
    // }


    // [HttpPost("/api/users/{identityKey}/permissions")]
    // [Tags("Users")]
    // public IActionResult AddUserPermission(string identityKey, [FromBody] AddPermissionDto data)
    // {
    //     service.AddUserPermission(identityKey, data.FeatureId, data.Action);
    //     return Ok();
    // }
    //
    
    // [HttpPut("/api/users/{identityKey}/permissions/{permissionId}")]
    // [Tags("Users")]
    // public IActionResult UpdateUserPermissions(int identityKey, int permissionId)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // [HttpDelete("/api/users/{identityKey}/permissions/{permissionId}")]
    // [Tags("Users")]
    // public IActionResult AddUserPermissions(int identityKey, int permissionId)
    // {
    //     throw new NotImplementedException();
    // }
    #endregion

    // #region Features
    //
    // [HttpPost("/api/features")]
    // [Tags("Features")]
    // public IActionResult AddFeature([FromBody] AddFeatureDto data)
    // {
    //     var feature = Feature.CreateFeature(data.Name, data.Key);
    //
    //     var result = service.AddFeature(feature);
    //
    //     return Ok(result);
    // }
    //
    // [HttpPost("/api/features/menus")]
    // [Tags("Features")]
    // public IActionResult AddFeatureMenu([FromBody] AddMenuDto data)
    // {
    //     var feature = Feature
    //         .CreateMenu(name: data.Name,
    //             key: data.Key,
    //             label: data.MenuLabel,
    //             path: data.MenuPath,
    //             icon: data.MenuIcon,
    //             order: data.MenuOrder);
    //     var result = service.AddFeature(feature);
    //
    //     return Ok(result);
    // }
    //
    // [HttpPut("/api/features/menus/{menuId}/children")]
    // [Tags("Features")]
    // public IActionResult AddFeatureMenuChildren(int menuId, [FromBody] AddMenuChildrenDto data)
    // {
    //     if (menuId != data.MenuId) return BadRequest();
    //     service.AddMenuChildren(data.MenuId, data.Children);
    //     return Ok();
    // }
    //
    // [HttpDelete("/api/features/menus/{menuId}/children/{childrenId}")]
    // [Tags("Features")]
    // public IActionResult RemoveFeatureMenuChildren(int menuId, int childrenId)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // [HttpPut("/api/features/{featureId}")]
    // [Tags("Features")]
    // public IActionResult UpdateFeature(int featureId, [FromBody] AddFeatureDto data)
    // {
    //     var feature = Feature.CreateFeature(data.Name, data.Key);
    //
    //     throw new NotImplementedException();
    // }
    //
    // [HttpPut("/api/features/{featureId}/status")]
    // [Tags("Features")]
    // public IActionResult UpdateFeatureStatus(int featureId, [FromBody] UpdateFeatureStatusDto data)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // [HttpPut("/api/features/menus/{menuId}")]
    // [Tags("Features")]
    // public IActionResult UpdateFeatureMenu(int menuId, [FromBody] AddMenuDto data)
    // {
    //     var feature = Feature
    //         .CreateMenu(name: data.Name,
    //             key: data.Key,
    //             label: data.MenuLabel,
    //             path: data.MenuPath,
    //             icon: data.MenuIcon,
    //             order: data.MenuOrder);
    //
    //     throw new NotImplementedException();
    // }
    //
    // #endregion

    
}

// public record RegisterUserDto(string IdentityKey, string Email);
//
// public record AddPermissionDto(int FeatureId, string Action);
//
// public record AddFeatureDto(string Name, string Key);
//
// public record UpdateFeatureStatusDto(bool IsActive);
//
// public record AddMenuDto(string Name, string Key, string MenuLabel, string MenuPath, string MenuIcon, int MenuOrder);
//
// public record AddMenuChildrenDto(int MenuId, HashSet<int> Children);