using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkBridgeModels
{
    public class InventoryDTO
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<long> ModifedBy { get; set; }
    }
}
