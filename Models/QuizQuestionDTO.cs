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
        public bool IsMatchQuestion { get; set; } = false;
        public List<MatchItemDTO>? MatchItems { get; set; }
        public AnswerExplanationDTO? AnsExplanation { get; set; }
            
    }
    
    public class MatchItemDTO
    {
        public required string Label { get; set; }
        public required string Text { get; set; }
        public bool IsLeftOption { get; set; }
    }
    
    public class AnswerExplanationDTO
    {
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}