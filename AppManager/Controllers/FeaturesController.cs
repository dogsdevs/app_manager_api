using AppManager.Domain;
using AppManager.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppManager.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class FeaturesController(ILogger<FeaturesController> logger, IFeatureService service, ITenantService tenantService)
    : ControllerBase
{

    [HttpGet]
    public IActionResult GetAll(string query = "", bool? isActive = null)
    {
        var features = service.GetAll(query, isActive)
            .Select(x => new FeatureDto(x.Id, x.Name, x.Key, x.MenuLabel, x.MenuPath, x.MenuIcon, x.MenuOrder, x.ShowInMenu, x.ParentId, x.IsActive));
        
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

        var dto = new FeatureDto(id, feature.Name, feature.Key, feature.MenuLabel, feature.MenuPath, feature.MenuIcon,
            feature.MenuOrder, feature.ShowInMenu, feature.ParentId, feature.IsActive);
        
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult CreateFeature([FromBody] CreateFeatureDto dto)
    {
        var feature = Feature.CreateFeature(dto.Name, dto.Key,  dto.MenuLabel, dto.MenuPath, dto.MenuIcon, dto.MenuOrder, dto.ShowInMenu, dto.ParentId, dto.IsActive);
        
        service.Create(feature);
        var featureDto = new FeatureDto(feature.Id, feature.Name, feature.Key, feature.MenuLabel, feature.MenuPath, feature.MenuIcon, feature.MenuOrder, feature.ShowInMenu, feature.ParentId, feature.IsActive);
        return CreatedAtAction(nameof(GetById), new { id = featureDto.Id }, featureDto);
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateFeature(int id, [FromBody] UpdateFeatureDto request)
    {
        var feature = service.GetById(id);

        if (feature == null)
        {
            return NotFound();
        }
        
        feature.Update(request.Name, request.Key, request.MenuLabel, request.MenuPath, request.MenuIcon,request.MenuOrder, request.ShowInMenu, request.ParentId,request.IsActive);
        service.Update(feature);
        
        return Ok(feature);
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

public record CreateFeatureDto(string Name, string Key, string MenuLabel, string MenuPath, string MenuIcon, int MenuOrder, bool ShowInMenu, int? ParentId, bool IsActive );
public record UpdateFeatureDto(string Name, string Key, string MenuLabel, string MenuPath, string MenuIcon, int MenuOrder, bool ShowInMenu, int? ParentId, bool IsActive );
public record FeatureDto(int Id, string Name, string Key, string MenuLabel, string MenuPath, string MenuIcon, int MenuOrder, bool ShowInMenu, int? ParentId, bool IsActive );