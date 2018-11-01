using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApi.Models
{
    public class Response
    {
        public int SurveyId { get; set; }
        public int ChoiceId { get; set; }
        public bool Option { get; set; }
    }
}
