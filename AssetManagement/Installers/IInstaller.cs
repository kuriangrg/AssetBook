﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Installers

{
    interface IInstaller
    {
        void InstallServices(IServiceCollection service, IConfiguration configuration);
    }
}
