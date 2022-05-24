using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agricathon_gr3.Models
{
    public class QuestionsWithAnwserModel
    {

        public int QuestionnaireId { get; set; }
        public List<string> Questions { get; set; }
        public List<string> Awnsers { get; set; }
    }
}
