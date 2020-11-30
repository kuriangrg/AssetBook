using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Contracts.V1.Requests
{
    public class CreateAssetRequest
    {
        public IFormFile FormFile { get; set; }
        public string ParentPath { get; set; }
        public int ParentAssetId { get; set; }

    }
}
