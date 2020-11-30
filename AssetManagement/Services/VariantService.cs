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
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace AssetManagement.Services
{
    public class VariantService : IVariantService
    {
        private readonly DataContext _dataContext;
        private readonly IComputerVisionClient _visionClient;
        private readonly IBlobService _blobService;
        private readonly AzureOptions _azureOptions;
        public VariantService(DataContext dataContext, 
            IComputerVisionClient visionClient, AzureOptions azureOptions,IBlobService blobService)
        {
            _azureOptions = azureOptions;
            _dataContext = dataContext;
            _visionClient = visionClient;
            _blobService = blobService;
        }

        public async Task CreateVariantsForAssetAsync(IFormFile file, int assetId)
        {
            var socialNetworks = _dataContext.Networks.Select(x=>x);
            foreach(var socialNetwork in socialNetworks)
            {
                var variant = new Variant
                {
                    AssetId=assetId,
                    NetworkId=socialNetwork.NetworkId,
                    UpdatedDate=DateTime.Now,
                    CreatedDdate=DateTime.Now,
                    VariantName=socialNetwork.NetworkName
                    
                };
                await SaveThumbnailMedia(socialNetwork,variant,file);
                _dataContext.Add<Variant>(variant);
               
            }
            await _dataContext.SaveChangesAsync();

        }

        public async Task<List<Variant>> GetVariantsFromAsset(int assetId)
        {
            return await _dataContext.Variants.Where(a => a.AssetId == assetId).ToListAsync<Variant>();
        }

        private async Task SaveThumbnailMedia(Network socialNetwork, Variant variant,IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            {
                var thumbnailStream 
                    = await _visionClient.GenerateThumbnailInStreamAsync(socialNetwork.MediaWidth, socialNetwork.MediaHeight, fileStream,true);
                var thumbNailUri = await _blobService.UploadMediaAsBlob(thumbnailStream,
                   _azureOptions.ThumbNailBlobContainer+socialNetwork.NetworkName.ToLower().Replace(" ",string.Empty), 
                   file.FileName,file.ContentType);
                variant.VariantDataLoc = thumbNailUri.LocalPath;
            }
        }
       
    }
}
