using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class Anwser
    {
        [Key]
        public int QuestionnaireId { get; set; }
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string Response { get; set; }

        public virtual Questionnaire Questionnaire { get; set; }
        public virtual Question Question { get; set; }
    }
}
