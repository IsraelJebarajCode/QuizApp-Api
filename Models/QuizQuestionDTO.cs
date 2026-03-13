using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizApi.Models.EnumModel;
using QuizApi.Models;

namespace QuizApi.Models
{
    public class QuizQuestionDTO
    {
        
        public required string Question { get; set; }
        public required string[] OptionValues { get; set; } = new string[4];
        public string CorrectOptionValue { get; set; }
        public string CorrectOptionType { get; set; } = "";
        public Part Section { get; set; }
        public GKUnit GKUnitNum { get; set; }
        public TamilUnit TamilUnitNum { get; set; }
        public MathsUnit MathsUnitNum { get; set; }        
    }
    
}