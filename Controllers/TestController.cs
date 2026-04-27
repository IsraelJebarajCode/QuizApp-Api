using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizApi.Data;
using QuizApi.Models;
using Microsoft.EntityFrameworkCore;

namespace QuizApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private QuizAppDbContext _dbContext;
        public TestController(QuizAppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllTests()
        {
            return Ok(_dbContext.TestData.ToList());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetTestDetailsById(Guid id)
        {
            var test = _dbContext.TestData.Find(id);
            if (test is null)
                return NotFound();
            return Ok(test);
        }

        [HttpGet]
        [Route("questions/{id:guid}")]
        public IActionResult GetTestById(Guid id)
        {
            List<Guid> QnIds = _dbContext.TestData.Find(id).QnIds;
            var TestQuestions = _dbContext.Quiz
                                     .Where(qn => QnIds.Contains(qn.Id))
                                     .Include(x => x.Options)
                                     .Include(y => y.QnCorrectOption)
                                     .Include(z => z.QnCategory)
                                     .ToList();

            return Ok(TestQuestions);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateTest(Test NewTest)
        {
            _dbContext.TestData.Add(NewTest);
            _dbContext.SaveChanges();
            return Ok(NewTest);
        }
        [HttpPut]
        public IActionResult UpdateTest(Test UpdatedTest)
        {
            _dbContext.TestData.Update(UpdatedTest);
            _dbContext.SaveChanges();
            return Ok(UpdatedTest);
        }

        [HttpDelete]
        public IActionResult DeleteTest(Guid id)
        {
            var test = _dbContext.TestData.Find(id);
            if (test is null)
                return NotFound();
            _dbContext.TestData.Remove(test);
            _dbContext.SaveChanges();
            return Ok(test);
        }
    }
}