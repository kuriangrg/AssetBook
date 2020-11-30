using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Contracts.V1.Requests;
using AssetManagement.Domain;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Services
{
    public interface IVariantService
    {
        Task CreateVariantsForAssetAsync(IFormFile file, int assetId);
        Task<List<Variant>> GetVariantsFromAsset(int assetId);

    }
}
