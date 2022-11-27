using JwtAuthenticationProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAndQuestionTypeController : ControllerBase
    {
        private readonly AuthApiDbContext _context;
        public QuestionAndQuestionTypeController(AuthApiDbContext context)
        {
            _context= context;
        }
        [HttpPost("{QuestionId}/{QuestionTypeId}")]
        public async Task<ActionResult> AddQuestionQuestionType(Guid QuestionId, Guid QuestionTypeId)
        {
           var question =  await _context.Questions
                .Include(c => c.QuestionTypes)
                .FirstOrDefaultAsync(item => item.QuestionId == QuestionId);

            if(question == null)
            {
                return NoContent();
            }

            var questionType = await _context.QuestionTypes
                .FirstOrDefaultAsync(item => item.QuestionTypeId == QuestionTypeId);

            if(questionType == null)
            {
                return NoContent();
            }

            question.QuestionTypes.Add(questionType);
            return Ok(await _context.SaveChangesAsync());
        }
    }
}
