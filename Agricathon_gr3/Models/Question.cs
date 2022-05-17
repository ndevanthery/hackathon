using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        public string NameQuestion { get; set; }
        
        public virtual List<Option> Options { get; set; }
        public virtual List<Anwser> Anwsers { get; set; }
    }
}
