using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        public string NameProject { get; set; }
        public string Place { get; set; }
        [Required]
        public int PhaseId { get; set; }

        public virtual List<PersProject> PersProjects { get; set; }
        public virtual Phase Phase { get; set; }
        public virtual List<Questionnaire> Questionnaire { get; set; }
    }
}
