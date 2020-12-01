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
using Azure.Storage.Blobs.Models;

namespace AssetManagement.Services
{
    public class AzureBlobService : IBlobService
    {
        private readonly AzureOptions _azureOptions;
        private readonly  BlobServiceClient _blobServiceClient;
        public AzureBlobService(BlobServiceClient blobServiceClient,  AzureOptions azureOptions)
        {
            _azureOptions = azureOptions;
            _blobServiceClient = blobServiceClient;
        }

        public async Task<BlobDownloadInfo> GetBlobAsync(string blobName, string container)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(container);
            var blobClient = containerClient.GetBlobClient(blobName);
            return  await blobClient.DownloadAsync();
           
        }

        public async Task<Uri> UploadMediaAsBlob(Stream stream, string containerName, string fileName, string contentType)
        {
           

            BlobContainerClient containerClient;
            try
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                containerClient.CreateIfNotExists();

                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = contentType });
                return blobClient.Uri;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }




        }
   
    }
}
