using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AssetManagement.Domain;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace AssetManagement.Services
{
    public interface IBlobService
    {
        Task<Uri> UploadMediaAsBlob(Stream stream, string containerName, string fullFileName, string contentType);

        Task<BlobDownloadInfo> GetBlobAsync(string blobName,string container);
    
    }
}
