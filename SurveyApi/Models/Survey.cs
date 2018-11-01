using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyApi.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public Choice Choice { get; set; }
    }
}
