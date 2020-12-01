using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AssetManagement.Data;
using AssetManagement.Services;

namespace AssetManagement.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<DBContextFactory, DBContextFactory>();
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IBlobService, AzureBlobService>();
            services.AddScoped<IVariantService, VariantService>();

        }
    }
}
