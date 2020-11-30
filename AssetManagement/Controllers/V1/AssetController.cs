using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AssetManagement.Contracts.V1;
using AssetManagement.Contracts.V1.Requests;
using AssetManagement.Domain;
using AssetManagement.Services;
using Microsoft.AspNetCore.Http;
using AssetManagement.option;
using AssetManagement.Contracts.V1.Responses;

namespace AssetManagement.Controllers.V1
{
    public class AssetController : Controller
    {
        private readonly IAssetService _assetService;
        private readonly IBlobService _blobService;
       
        private readonly IVariantService _variantService;

        public AssetController(IAssetService assetService,
            IBlobService blobService, IVariantService variantService)
        {
            _assetService = assetService;
            _blobService = blobService;
            _variantService = variantService;

        }

        /// <summary>
        /// Retrieves the asset and immediate 2 level of children
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Assets.GetAssetAndChildren)]
        public async Task<IActionResult> GetAssetAndChildren([FromRoute] int assetId)
        {
            try
            {
                var asset = await _assetService.GetAssetById(assetId, HttpContext.Request);
                if (asset == null)
                {
                    return NotFound();
                }
                return Ok(asset);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get the asset details along with variant details
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.Assets.GetAssetDetails)]
        public async Task<IActionResult> GetAssetDetails([FromRoute] int assetId)
        {
            try
            {
                var asset = await _assetService.GetAssetById(assetId, HttpContext.Request);
                if (asset == null)
                {
                    return NotFound();
                }
                var variatns = await _variantService.GetVariantsFromAsset(assetId);
                return Ok(new VariantResponse { Asset = asset, Variants = variatns });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Create an image or video
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Assets.CreateMediaAsset)]
        public async Task<IActionResult> SaveAsset([FromForm] CreateAssetRequest request)
        {
            try
            {
                var asset = await _assetService.CreateAssetAsync(request, HttpContext.Request);
                return Ok(asset);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpPost(ApiRoutes.Assets.CreateFolder)]
        public async Task<IActionResult> CreateFolderAsset([FromBody] CreateFolderRequest folderRequest)
        {
            try
            {
                var asset = new Asset
                {
                    AssetName = folderRequest.FolderName,
                    CreatedDdate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsFolder = true
                };
                if (folderRequest.ParentAssetId != 0)
                {
                    asset.ParentAssetId = folderRequest.ParentAssetId;
                }

                await _assetService.CreateFolderAsync(asset);
                return Ok(asset);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpGet(ApiRoutes.Assets.GetMediaBlob)]
        public async Task<IActionResult> GetBlob([FromRoute] string container, string blobPath)
        {
            try
            {
                var blob = await _blobService.GetBlobAsync(blobPath, container);
                if (blob == null)
                {
                    return NotFound();
                }
                return File(blob.Content, blob.ContentType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

    }
}
