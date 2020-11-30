using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Domain
{
    public class Network
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int NetworkId { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string NetworkName { get;  set; }
        public int MediaHeight { get; set; }
        public int MediaWidth { get; set; }
    }
}
