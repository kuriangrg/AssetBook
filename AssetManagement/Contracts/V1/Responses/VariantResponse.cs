using AssetManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Contracts.V1.Responses
{
    public class VariantResponse
    {
        public Asset Asset { get; set; }
        public List<Variant> Variants { get; set; }

    }
}
