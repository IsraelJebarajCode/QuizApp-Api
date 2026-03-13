using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizApi.Models.EnumModel;

namespace QuizApi.Models
{
    public class Test
    {
        public Guid Id { get; set; }
        public string TestName { get; set; }
        public string? TestDescription { get; set; } = string.Empty;
        public bool IsSectionTest { get; set; }
        public Part? Section { get; set; }
        public int? TotalTimeInMins { get; set; }
        public List<Guid> QnIds { get; set; }
    }
}