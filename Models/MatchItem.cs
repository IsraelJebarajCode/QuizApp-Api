using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class MatchItem
    {
        public Guid Id { get; set; }
        [ForeignKey("MatchQuestion")]
        public Guid MatchQuestionId { get; set; }
        public bool IsLeftOption { get; set; }
        public required string Label { get; set; }
        public required string Text { get; set; }
    }
}