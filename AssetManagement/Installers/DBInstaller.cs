using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IBlobService, AzureBlobService>();
            services.AddScoped<IVariantService, VariantService>();

        }
    }
}
