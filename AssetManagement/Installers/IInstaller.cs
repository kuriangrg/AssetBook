using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AssetManagement.Installers

{
    interface IInstaller
    {
        void InstallServices(IServiceCollection service, IConfiguration configuration);
    }
}
