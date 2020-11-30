using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Domain
{
    public class Asset
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int AssetId { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        [Required]
        public string AssetName { get;  set; }
        [Column(TypeName = "nvarchar(300)")]
        public string AssetDataLoc { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string Thumbnail { get; set; }
        public string PathName { get; set; }
       
        public DateTime CreatedDdate { get; set; }
       
        public DateTime UpdatedDate { get; set; }
        public bool IsFolder { get; set; }
        public int? ParentAssetId { get; set; }
        [ForeignKey("ParentAssetId")]
        public List<Asset> Children { get; set; }

    }
}
