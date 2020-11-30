using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";

        public  const  string Base = Root+"/"+Version;

        public static class Assets
        {
            public const  string GetAll= Base+"/Assets";
            public const string CreateMediaAsset = Base + "/Assets/Create";
            public const string CreateFolder = Base + "/Assets/CreateFolder";
            public const string GetAssetAndChildren = Base + "/Assets/{assetId}";

            public const string GetAssetDetails = Base + "/Assets/GetAssetDetails/{assetId}";
            public const string Update = Base + "/Assets/{assetId}";
            public const string GetMediaBlob = Base + "/Assets/blobstore/{container}/{blobPath}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string Refresh = Base + "/identity/refreshToken";

        }

    }
}
