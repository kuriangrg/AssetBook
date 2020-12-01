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

namespace AssetManagement.Services
{
    public class VariantService : IVariantService
    {
        private readonly DataContext _dataContext;
        private readonly IComputerVisionClient _visionClient;
        private readonly IBlobService _blobService;
        private readonly AzureOptions _azureOptions;
        public VariantService(DataContext dataContext,
            IComputerVisionClient visionClient, AzureOptions azureOptions
            , IBlobService blobService)
        {
            _azureOptions = azureOptions;
            _dataContext = dataContext;
            _visionClient = visionClient;
            _blobService = blobService;
        }

        public async Task CreateVariantsForAssetAsync(IFormFile file, int assetId)
        {
            try
            {
                var socialNetworks = _dataContext.Networks.Select(x => x);
                foreach (var socialNetwork in socialNetworks)
                {
                    string variantContainer = _azureOptions.ThumbNailBlobContainer +
                        socialNetwork.NetworkName.ToLower().Replace(" ", string.Empty);
                    string azureFileName = file.FileName.PrependGuid();

                    var variant = new Variant
                    {
                        AssetId = assetId,
                        NetworkId = socialNetwork.NetworkId,
                        UpdatedDate = DateTime.Now,
                        CreatedDdate = DateTime.Now,
                        VariantName = socialNetwork.NetworkName,
                        VariantDataLoc = $"/{variantContainer}/{azureFileName}"
                    };
                    _dataContext.Add<Variant>(variant);

                    using (var fileStream = file.OpenReadStream())
                    {
                        var thumbnailStream
                            = await _visionClient.GenerateThumbnailInStreamAsync(socialNetwork.MediaWidth,
                       socialNetwork.MediaHeight, fileStream, true);
                        //Execution as background task
                        _ = Task.Run(() => _blobService.UploadMediaAsBlob(thumbnailStream, variantContainer, azureFileName, file.ContentType));

                    }
                }
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task<List<Variant>> GetVariantsFromAsset(int assetId)
        {
            return await _dataContext.Variants.Where(a => a.AssetId == assetId).ToListAsync<Variant>();
        }

    }
}