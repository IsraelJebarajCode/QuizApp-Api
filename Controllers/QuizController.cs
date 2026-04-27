using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Models;
using QuizApi.Data;
using Microsoft.EntityFrameworkCore;
using QuizApi.Models.EnumModel;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private QuizAppDbContext _dbContext;
        public QuizController(QuizAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetQuizQuestions(){
            var Questions = _dbContext.Quiz
                    .Include(x=>x.Options)
                    .Include(y=>y.QnCorrectOption)
                    .Include(z=>z.QnCategory)
                    .Include(m => m.MatchQuestion)
                    .ThenInclude(mi=>mi.MatchItems)
                    .Include(e=>e.AnsExplanation)
                    .ToList();
            return Ok(Questions);
        }
        [HttpPost]
        [Route("getsectionqn/{sectionid}")]
        public IActionResult GenarateQuiz(Part sectionid){
            var SecQns = _dbContext.Quiz
                             .Where(qu=>qu.QnCategory.Section==sectionid)
                             .Include(x =>x.QnCorrectOption)
                             .Include(y=>y.Options) 
                             .Include(z=>z.QnCategory)                            
                             .ToList();


            return Ok(SecQns);
        }

        [HttpPost]
        public IActionResult AddQuestion(QuizQuestionDTO QuizQuestion){
            Guid questionId = Guid.NewGuid();
            List<Option> optionsList = new List<Option>();
            
            CorrectOption cp = new CorrectOption();
            for(int i=0 ;i<4;i++)
            {  
                Option opt = new Option();
                opt.OptionValue = QuizQuestion.OptionValues[i];
                opt.OptionId=Guid.NewGuid();
                if( QuizQuestion.CorrectOptionValue!= null && String.Equals(opt.OptionValue,QuizQuestion.CorrectOptionValue)){
                    cp.CorrOptionId =opt.OptionId;
                }
                optionsList.Add(opt);
            }
            cp.CorrectOptionId=Guid.NewGuid();
            cp.CorrectOptionValue = QuizQuestion.CorrectOptionValue;
            
            Quiz newQuestion = new Quiz(){
                Id= questionId,
                Question= QuizQuestion.Question,
                IsMatchQuestion= QuizQuestion.IsMatchQuestion,
                Options= optionsList,
                QnCorrectOption= cp,
                CorrectOptionType= QuizQuestion.CorrectOptionType
            };
            
            // Conditionally create Category only if Section is not 0 and not NA
            if(QuizQuestion.Section != 0 && QuizQuestion.Section != Part.NA)
            {
                newQuestion.QnCategory = new Category(){
                    Section=QuizQuestion.Section,
                    GKUnitName=QuizQuestion.GKUnitNum,
                    TamilUnitName=QuizQuestion.TamilUnitNum,
                    MathsUnitName=QuizQuestion.MathsUnitNum
                };
            }
            
            // If Match question, fill MatchQuestion object
            if(QuizQuestion.IsMatchQuestion && QuizQuestion.MatchItems != null)
            {
                List<MatchItem> matchItems = new List<MatchItem>();
                foreach(var item in QuizQuestion.MatchItems)
                {
                    matchItems.Add(new MatchItem(){
                        Id = Guid.NewGuid(),
                        MatchQuestionId = questionId,
                        Label = item.Label,
                        Text = item.Text,
                        IsLeftOption = item.IsLeftOption
                    });
                }
                
                newQuestion.MatchQuestion = new MatchQuestion(){
                    Id = Guid.NewGuid(),
                    QnId = questionId,
                    MatchItems = matchItems
                };
            }
            
            // If AnswerExplanation provided, add it
            if(QuizQuestion.AnsExplanation != null && 
               (!string.IsNullOrEmpty(QuizQuestion.AnsExplanation.Image) || 
                !string.IsNullOrEmpty(QuizQuestion.AnsExplanation.Description)))
            {
                newQuestion.AnsExplanation = new AnswerExplanation(){
                    Id = Guid.NewGuid(),
                    Image = !string.IsNullOrEmpty(QuizQuestion.AnsExplanation.Image) ? QuizQuestion.AnsExplanation.Image : null,
                    Description = !string.IsNullOrEmpty(QuizQuestion.AnsExplanation.Description) ? QuizQuestion.AnsExplanation.Description : null
                };
            }
                       
            _dbContext.Quiz.Add(newQuestion);
            _dbContext.SaveChanges();
            return Ok(newQuestion);
        }

        [HttpGet]
        [Route("match-qns")]
        public IActionResult GetAllMatchQuestions(){
            var matchQns = _dbContext.Quiz
                            .Where(q => q.IsMatchQuestion == true)
                            .Include(m => m.MatchQuestion)
                            .ThenInclude(mi => mi.MatchItems)
                            .ToList();
            return Ok(matchQns);
        }

        [HttpPost]
        [Route("upload-multiple")]
        public IActionResult UploadMulitpleQuestion(List<QuizQuestionDTO> questions)
        {
            try
            {
                questions.ForEach(question =>{
                    AddQuestion(question);                
                });
                return Ok();
            }
            catch(System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("export")]
        public IActionResult ExportAllQuestions(){
            var exportData = _dbContext.Quiz
                    .Select(quiz => new QuizQuestionDTO{
                        Question = quiz.Question,
                        OptionValues = quiz.Options.Select(o => o.OptionValue).ToArray(),
                        CorrectOptionValue = quiz.QnCorrectOption.CorrectOptionValue,
                        CorrectOptionType = quiz.CorrectOptionType,
                        IsMatchQuestion = false,
                        MatchItems = null,
                        Section = quiz.QnCategory.Section,
                        GKUnitNum = quiz.QnCategory.GKUnitName,
                        TamilUnitNum = quiz.QnCategory.TamilUnitName,
                        MathsUnitNum = quiz.QnCategory.MathsUnitName,
                        // AnsExplanation = quiz.AnsExplanation != null ? new AnswerExplanationDTO{
                        //     Image = quiz.AnsExplanation.Image,
                        //     Description = quiz.AnsExplanation.Description
                        // } : null
                    })
                    .ToList();
            
            return Ok(exportData);
        }

        [HttpDelete]
        public IActionResult DeleteQuestion(Guid id){
            var  ques = _dbContext.Quiz.Find(id);                     
            if(ques is null)
                return NotFound();
            _dbContext.Quiz.Remove(ques);
            _dbContext.SaveChanges();
            return Ok(ques);
        }
        [HttpPut]
        public IActionResult UpdateQuestion(Quiz UpdatedQueston)
        {
            _dbContext.Quiz.Update(UpdatedQueston);
            _dbContext.SaveChanges();
            return Ok(UpdatedQueston);
        }
    }
}