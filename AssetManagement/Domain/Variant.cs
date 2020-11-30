using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagement.Domain
{
    public class Variant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int VariantId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string VariantName { get;  set; }
        [Column(TypeName = "varchar(400)")]
        public string VariantDataLoc { get; set; }
        [ForeignKey("Asset")]
        public int AssetId { get; set; } 
        public Asset Asset { get; set; }
        [ForeignKey("Network")]
        public int NetworkId { get; set; }
        public Network Network { get; set; }
        public DateTime CreatedDdate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
