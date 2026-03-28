namespace AppManager.Domain.Services;

public interface IPermissionsService
{
    void DeleteByFeatureId(int featureId);
}