using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QuizApi.Models
{
    public class Option
    {
        public Guid OptionId { get; set; }
        public string OptionValue { get; set; } ="";    
    }
}