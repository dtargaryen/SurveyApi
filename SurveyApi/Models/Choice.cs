using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApi.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public bool Option { get; set; }
        public Survey Survey { get; set; }
    }
}
