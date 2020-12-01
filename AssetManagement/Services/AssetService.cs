using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Data;
using AssetManagement.Domain;
using AssetManagement.option;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using AssetManagement.Contracts.V1.Requests;

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
            IComputerVisionClient visionClient, AzureOptions azureOptions, IBlobService blobService,
            IVariantService variantService)
        {
            _azureOptions = azureOptions;
            _dataContext = dataContext;
            _visionClient = visionClient;
            _blobService = blobService;
            _variantService = variantService;
        }

        public async Task<Asset> CreateAssetAsync(CreateAssetRequest request, HttpRequest httpRequest)
        {
            string azureAssetFileName = request.FormFile.FileName.PrependGuid();
            string azureThumbnailFileName = request.FormFile.FileName.PrependGuid();

            var asset = new Asset
            {
                AssetName = request.FormFile.FileName,
                CreatedDdate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                PathName = request.ParentPath,
                IsFolder = false,
                ParentAssetId = request.ParentAssetId,
                AssetDataLoc = $"/{_azureOptions.MediaBlobContainer}/{azureAssetFileName}",
                Thumbnail = $"/{_azureOptions.ThumbNailBlobContainer}/{azureThumbnailFileName}"
            };
            await SaveThumbnailMedia(request.FormFile, azureThumbnailFileName);
            await _dataContext.Assets.AddAsync(asset);
            var created = await _dataContext.SaveChangesAsync();

            //The parellel task is run inside create variant service
            await _variantService.CreateVariantsForAssetAsync(request.FormFile, asset.AssetId);

            //Parlele task to save actual data to azure blob
            var memStream = new MemoryStream();
            request.FormFile.CopyTo(memStream);
             _ = Task.Run(() => SaveOriginalMedia(request, asset, memStream, azureAssetFileName));
            //TO DO add meta data

            return asset;
        }

        private async Task SaveThumbnailMedia(IFormFile file, string fileName)
        {
            using (var fileStream = file.OpenReadStream())
            {
                var thumbnailStream = await _visionClient.GenerateThumbnailInStreamAsync(200, 200, fileStream, true);
                var thumbNailUri = await _blobService.UploadMediaAsBlob(thumbnailStream,
                   _azureOptions.ThumbNailBlobContainer, fileName, file.ContentType);
            }
        }

        private async Task SaveOriginalMedia(CreateAssetRequest request, Asset asset, Stream mediaMemStream, string azureFilename)
        {
            try
            {
                mediaMemStream.Position = 0;
                await _blobService.UploadMediaAsBlob(mediaMemStream,
                      _azureOptions.MediaBlobContainer, azureFilename, request.FormFile.ContentType);
                mediaMemStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task<bool> CreateFolderAsync(Asset asset)
        {
            await _dataContext.Assets.AddAsync(asset);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        /// <summary>
        /// TODO Pagination
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Asset> GetAssetById(int postId, HttpRequest request)
        {
            Asset asset = null;
            if (postId == 0)
            {
                asset = await _dataContext.Assets.Include(x => x.Children).
                    ThenInclude(x => x.Children).OrderBy(x => x.AssetId).Where(x => x.ParentAssetId == null).FirstOrDefaultAsync();
            }
            else
            {
                asset = await _dataContext.Assets.Include(x => x.Children).ThenInclude(x => x.Children).SingleOrDefaultAsync(x => x.AssetId == postId);
            }
            return asset;
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