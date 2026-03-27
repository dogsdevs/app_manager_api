namespace AppManager.Domain.Services;

public interface IFeatureService : BaseCrudService<Feature>
{
    IEnumerable<Feature> GetAll(string query, bool? isActive);
}