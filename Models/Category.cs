using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizApi.Models.EnumModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public Part Section { get; set; } = Part.NA;
        public GKUnit GKUnitName { get; set; } = GKUnit.NA;
        public TamilUnit TamilUnitName { get; set; } = TamilUnit.NA;
        public MathsUnit MathsUnitName { get; set; } = MathsUnit.NA;
        public Standard SclBookStandard { get; set; } = Standard.NA;
        [ForeignKey("Quiz")]
        public Guid QnId { get; set; }
    }
}