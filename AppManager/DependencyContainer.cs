using AppManager.Domain.Services;
using AppManager.Infrastructure;
using AppManager.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppManager;

public static class DependencyContainer
{
    public static void AddAppManager(this IServiceCollection services)
    {
        services.AddTransient<IRoleService, RoleService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IFeatureService, FeatureService>();
        services.AddTransient<IAppManagerService, AppManagerService>();
        services.AddTransient<ITenantService, TenantService>();
        services.AddTransient<IPermissionsService, PermissionsService>();
    }
}