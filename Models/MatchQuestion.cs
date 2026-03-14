using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class MatchQuestion
    {
        public Guid Id { get; set; }
        [ForeignKey("Quiz")]
        public Guid QnId { get; set; }
        public List<MatchItem>? MatchItems { get; set; }
    }
}