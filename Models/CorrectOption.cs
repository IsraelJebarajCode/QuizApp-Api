using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class CorrectOption
    {
        public Guid CorrectOptionId { get; set; }
        public string CorrectOptionValue { get; set; }
        public Guid CorrOptionId { get; set; }  
        [ForeignKey("Quiz")]
        public Guid QnId { get; set; }
    }
}