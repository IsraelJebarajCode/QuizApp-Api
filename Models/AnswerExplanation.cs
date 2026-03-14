using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class AnswerExplanation
    {
        public Guid Id { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Quiz")]
        public Guid QnId { get; set; }
    }
}
