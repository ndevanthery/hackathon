using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }
        [Required]
        public string NameOption { get; set; }
        [Required]
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
