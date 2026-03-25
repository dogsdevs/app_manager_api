namespace AppManager.Domain.Services;

public interface IUserService : BaseCrudService<User>
{
    IEnumerable<User> GetAll(string query, bool? isActive);
}