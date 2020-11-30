using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AssetManagement.Data;
using AssetManagement.Domain;
using AssetManagement.option;
using Azure.Storage.Blobs;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using AssetManagement.Contracts.V1.Requests;
using System.Web;

namespace AssetManagement.Services
{
    public class AssetService : IAssetService
    {
        private readonly DataContext _dataContext;
        private readonly IComputerVisionClient _visionClient;
        private readonly IBlobService _blobService;
        private readonly IVariantService _variantService;
        private readonly AzureOptions _azureOptions;
        public AssetService(DataContext dataContext, 
            IComputerVisionClient visionClient, AzureOptions azureOptions,IBlobService blobService,IVariantService variantService)
        {
            _azureOptions = azureOptions;
            _dataContext = dataContext;
            _visionClient = visionClient;
            _blobService = blobService;
            _variantService = variantService;
        }

        public async Task<Asset> CreateAssetAsync(CreateAssetRequest request, HttpRequest httpRequest)
        {
            var asset = new Asset
            {
                AssetName = request.FormFile.FileName,
                CreatedDdate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                PathName = request.ParentPath,
                IsFolder = false
            };
            
            await SaveThumbnailMedia(request, asset);
            if (request.ParentAssetId != 0)
            {
                asset.ParentAssetId = request.ParentAssetId;
            }
            await _dataContext.Assets.AddAsync(asset);
            var created = await _dataContext.SaveChangesAsync();

            await Task.Run(() => SaveOriginalMedia(request, asset));
            await Task.Run(() => _variantService.CreateVariantsForAssetAsync(request.FormFile, asset.AssetId));
            //TO DO add meta data

            return asset;
        }

        private async Task SaveThumbnailMedia(CreateAssetRequest request, Asset asset)
        {
            using (var fileStream = request.FormFile.OpenReadStream())
            {
                var thumbnailStream = await _visionClient.GenerateThumbnailInStreamAsync(200, 200, fileStream,true);
                var thumbNailUri = await _blobService.UploadMediaAsBlob(thumbnailStream,
                   _azureOptions.ThumbNailBlobContainer, request.FormFile.FileName, request.FormFile.ContentType);
                asset.Thumbnail = thumbNailUri.LocalPath;
            }
        }

        private async Task SaveOriginalMedia(CreateAssetRequest request, Asset asset)
        {
            using (var fileStream = request.FormFile.OpenReadStream())
            {
                var mediaUri = await _blobService.UploadMediaAsBlob(fileStream,
                  _azureOptions.MediaBlobContainer, request.FormFile.FileName, request.FormFile.ContentType);
                asset.AssetDataLoc = mediaUri.LocalPath;
                _dataContext.Assets.Update(asset);
               await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<bool> CreateFolderAsync(Asset asset)
        {
            await _dataContext.Assets.AddAsync(asset);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;

        }
        public async Task<Asset> GetAssetById(int postId,HttpRequest request)
        {
            Asset asset = null;
            if (postId == 0)
            {   
                asset = await _dataContext.Assets.Include(x => x.Children).
                    ThenInclude(x => x.Children).OrderBy(x=>x.AssetId).Where(x=>x.ParentAssetId==null).FirstOrDefaultAsync();
            }
            else
            {
                asset = await _dataContext.Assets.Include(x => x.Children).ThenInclude(x=>x.Children).SingleOrDefaultAsync(x => x.AssetId == postId);
            }
            return asset;
        }

        public async Task<Asset> GetAssetsFromRoot(int postId)
        {
            return await _dataContext.Assets.
                Where(x => x.AssetId == postId).SingleOrDefaultAsync();
        }

        Task<List<Asset>> IAssetService.GetAssetsFromRoot(int rootID)
        {
            throw new NotImplementedException();
        }
      
        public async Task<bool> UpdateAsset(Asset assetToUpdate)
        {
            _dataContext.Assets.Update(assetToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }
    }
}
