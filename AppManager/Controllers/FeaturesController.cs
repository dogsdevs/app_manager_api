using System.ComponentModel.DataAnnotations;
using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppManager.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class FeaturesController(
    ILogger<FeaturesController> logger,
    IFeatureService service,
    ITenantService tenantService)
    : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll(string query = "", bool? isActive = null)
    {
        var features = service.GetAll(query, isActive)
            .Select(x => new FeatureWCountDto(x.Id,
                x.Name,
                x.Key,
                x.MenuLabel,
                x.MenuPath,
                x.MenuIcon,
                x.MenuOrder,
                x.ShowInMenu,
                x.ParentId,
                x.IsActive,
                x.PermissionsCount
            ));
        return Ok(features);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var feature = service.GetById(id);

        if (feature == null)
        {
            return NotFound();
        }

        var dto = new FeatureDto(id,
            feature.Name,
            feature.Key,
            feature.MenuLabel,
            feature.MenuPath,
            feature.MenuIcon,
            feature.MenuOrder,
            feature.ShowInMenu,
            feature.ParentId,
            feature.IsActive,
            feature.Permissions.Select(x => new PermissionDto(x.Id, x.GuardName, x.Name, x.Action, x.Description))
                .ToArray()
        );

        return Ok(dto);
    }

    [HttpPost]
    public IActionResult CreateFeature([FromBody] CreateFeatureDto dto)
    {
        try
        {
            var permissions = dto.Permissions.Select(x =>
                Permission.Create(x.Id, null, x.GuardName, $"{dto.Key}.{x.Action}", x.Action, x.Description)).ToArray();
            var feature = Feature.CreateFeature(dto.Name, dto.Key, dto.MenuLabel, dto.MenuPath, dto.MenuIcon,
                dto.MenuOrder, dto.ShowInMenu, dto.ParentId, dto.IsActive);
            feature.ManagePermission(permissions);

            service.Create(feature);
            var featureDto = new FeatureDto(feature.Id,
                feature.Name,
                feature.Key,
                feature.MenuLabel,
                feature.MenuPath,
                feature.MenuIcon,
                feature.MenuOrder,
                feature.ShowInMenu,
                feature.ParentId,
                feature.IsActive,
                feature.Permissions.Select(x => new PermissionDto(x.Id, x.GuardName, x.Name, x.Action, x.Description))
                    .ToArray()
            );
            return CreatedAtAction(nameof(GetById), new { id = featureDto.Id }, featureDto);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFeature(int id, [FromBody] UpdateFeatureDto request)
    {
        try
        {
            var feature = service.GetById(id);

            if (feature == null)
            {
                return NotFound();
            }

            var permissions = request.Permissions.Select(x =>
                    Permission.Create(x.Id, id, x.GuardName, $"{request.Key}.{x.Action}", x.Action, x.Description))
                .ToArray();

            feature.Update(request.Name,
                request.Key,
                request.MenuLabel,
                request.MenuPath,
                request.MenuIcon,
                request.MenuOrder,
                request.ShowInMenu,
                request.ParentId,
                request.IsActive,
                permissions
            );

            service.Update(feature);

            var featureDto = new FeatureDto(feature.Id,
                feature.Name,
                feature.Key,
                feature.MenuLabel,
                feature.MenuPath,
                feature.MenuIcon,
                feature.MenuOrder,
                feature.ShowInMenu,
                feature.ParentId,
                feature.IsActive,
                feature.Permissions.Select(x => new PermissionDto(x.Id, x.GuardName, x.Name, x.Action, x.Description))
                    .ToArray()
            );

            return Ok(featureDto);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFeature(int id)
    {
        var feature = service.GetById(id);

        if (feature == null)
        {
            return NotFound();
        }

        service.Delete(feature);

        return NoContent();
    }
}

public record CreateFeatureDto(
    string Name,
    string Key,
    string MenuLabel,
    string MenuPath,
    string MenuIcon,
    int MenuOrder,
    bool ShowInMenu,
    int? ParentId,
    bool IsActive,
    PermissionDto[] Permissions);

public record UpdateFeatureDto(
    string Name,
    string Key,
    string MenuLabel,
    string MenuPath,
    string MenuIcon,
    int MenuOrder,
    bool ShowInMenu,
    int? ParentId,
    bool IsActive,
    PermissionDto[] Permissions);

public record FeatureDto(
    int Id,
    string Name,
    string Key,
    string MenuLabel,
    string MenuPath,
    string MenuIcon,
    int MenuOrder,
    bool ShowInMenu,
    int? ParentId,
    bool IsActive,
    PermissionDto[] Permissions);

public record FeatureWCountDto(
    int Id,
    string Name,
    string Key,
    string MenuLabel,
    string MenuPath,
    string MenuIcon,
    int MenuOrder,
    bool ShowInMenu,
    int? ParentId,
    bool IsActive,
    int PermissionsCount);

public record PermissionDto(int? Id, string GuardName, string Name, string Action, string Description);