﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using AssetManagement.option;
using AssetManagement.Services;
using Azure.Storage.Blobs;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;

namespace AssetManagement.Installers
{
    public class MVCInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    
                    Title = "Dirico API",
                    Version = "V1"

                });
            });

            var azureOptions = new AzureOptions();
            configuration.Bind(nameof(azureOptions), azureOptions);
            services.AddSingleton(azureOptions);
            services.AddSingleton(x => new BlobServiceClient(azureOptions.AccessKey));

            services.AddScoped<IComputerVisionClient>(factory => {
                var key = azureOptions.ComputerVisionKey;
                var host = azureOptions.ComputerVisionEndpoint;

                var credentials = new ApiKeyServiceClientCredentials(key);
                var client = new ComputerVisionClient(credentials);
                client.Endpoint = host;

                return client;
            });

        }

    }
}
