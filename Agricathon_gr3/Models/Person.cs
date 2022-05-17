using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int TypePId { get; set; }
        public string Tel { get; set; }

        public virtual TypePerson TypePerson { get; set; }
        public virtual List<PersProject> PersProjects { get; set; }
        public virtual List<Questionnaire> Questionnaires { get; set; }

    }
}
