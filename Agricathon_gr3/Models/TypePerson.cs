using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Agricathon_gr3.Models
{
    public class TypePerson
    {
        [Key]
        public int TypePId { get; set; }
        [Required]
        public string TypeP { get; set; }
    }
}