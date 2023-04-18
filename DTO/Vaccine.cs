using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL3.DTO
{
    public class Vaccine
    {
        [Key]
        [StringLength(50)]
        [Required]
        public string vaccineName { get; set; }
        public int quantity { get; set; }
        
    }
}
