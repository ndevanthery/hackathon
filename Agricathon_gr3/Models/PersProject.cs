using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class PersProject
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        public int ProjetId { get; set; }

        public virtual Project Project { get; set; }
    }
}
