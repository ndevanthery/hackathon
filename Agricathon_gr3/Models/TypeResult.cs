using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class TypeResult
    {
        [Key]
        public int TypeRId { get; set; }
        [Required]
        public string TypeR { get; set; }

        public List<Questionnaire> Questionnaires { get; set; }
    }
}
