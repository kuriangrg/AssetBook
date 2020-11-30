
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetManagement.Domain
{
    public class Metadata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int MetadataId { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string Caption { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string Location { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string TagNames { get; set; }
        public string CreationDate { get; set; }
        public string UpdationDate { get; set; }

    }
}
