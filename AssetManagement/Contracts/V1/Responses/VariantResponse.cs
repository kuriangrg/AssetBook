using AssetManagement.Domain;
using System.Collections.Generic;

namespace AssetManagement.Contracts.V1.Responses
{
    public class VariantResponse
    {
        public Asset Asset { get; set; }
        public List<Variant> Variants { get; set; }

    }
}
