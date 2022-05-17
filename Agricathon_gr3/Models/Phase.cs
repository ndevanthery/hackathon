using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class Phase
    {
        [Key]
        public int PhaseId { get; set; }
        [Required]
        public string NamePhase { get; set; }

        public virtual List<Project> Projects { get; set; }
        public virtual List<Questionnaire> Questionnaires { get; set; }
    }
}
