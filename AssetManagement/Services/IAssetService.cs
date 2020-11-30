using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Contracts.V1.Requests;
using AssetManagement.Domain;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Services
{
    public interface IAssetService
    {
      
        Task<Asset> GetAssetById(int assetId, HttpRequest request);
        Task<List<Asset>> GetAssetsFromRoot(int rootID);
        Task<Asset> CreateAssetAsync(CreateAssetRequest request, HttpRequest httpRequest);
        Task<bool> UpdateAsset(Asset assetToUpdate);
        Task<bool> CreateFolderAsync(Asset asset);


    }
}
