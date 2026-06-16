using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class AnswerKeyExplanation
    {
        public int Id { get; set; }

        [ForeignKey("Quiz")]
        public Guid QuestionId { get; set; }

        public string AnswerKeyExplanationHtml { get; set; } = string.Empty;
    }
}