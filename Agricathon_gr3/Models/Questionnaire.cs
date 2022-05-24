using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class Questionnaire
    {
        [Key]
        public int QuestionnaireId { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        public string NameQuestionnaire { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string PersonId { get; set; }
        [Required]
        public int PhaseId { get; set; }
        [Required]
        public int TypeRId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Phase Phase { get; set; }
        public virtual TypeResult TypeResult { get; set; }
        public virtual List<Anwser> Anwsers { get; set; }
    }
}
