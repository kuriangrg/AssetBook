using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Contracts.V1.Requests
{
    public class CreateFolderRequest
    {
        public string FolderName { get; set; }
        public string ParentPathName { get; set; }
        public int ParentAssetId { get; set; }

    }
}
