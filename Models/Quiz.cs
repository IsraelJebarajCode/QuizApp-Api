using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.Models
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public required string Question { get; set; }
        public bool? IsMatchQuestion {get;set;}
        public MatchQuestion? MatchQuestion { get; set; }
        public List<Option> Options { get; set; }        
        public CorrectOption QnCorrectOption { get; set; }
        public required string CorrectOptionType { get; set; }
        public Category? QnCategory { get; set; }
      }
}