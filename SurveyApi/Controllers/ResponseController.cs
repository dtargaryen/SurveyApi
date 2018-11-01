using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyApi.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : Controller
    {
        private readonly SurveyContext _context;

        public ResponseController(SurveyContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Response>> GetAll()
        {
            return _context.Responses.ToList();
        }
        
        [HttpGet("{surveyId}/{choiceId}", Name = "GetResult")]
        public ActionResult<Response> GetById(int surveyId, int choiceId)
        {
            var result = _context.Responses
                .Where(x => x.SurveyId == surveyId)
                .Single(x => x.ChoiceId == choiceId);
            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
        
        [HttpPost("{surveyId}/{choiceId}")]
        public IActionResult Create(int surveyId, int choiceId, Response questionGuest)
        {
            questionGuest.SurveyId = surveyId;
            questionGuest.ChoiceId = choiceId;
            questionGuest.Option = true;

            _context.Responses.Add(questionGuest);
            _context.SaveChanges();

            return CreatedAtRoute("GetResult", new { surveyId = questionGuest.SurveyId, choiceId = questionGuest.ChoiceId }, questionGuest);
        }
        
        [HttpPut("{surveyId}/{choiceId}")]
        public IActionResult Update(int surveyId, int choiceId, Response questionGuest)
        {
            var response = _context.Responses
                .Where(x => x.SurveyId == surveyId)
                .Single(x => x.ChoiceId == choiceId);
            if (response == null)
            {
                return NotFound();
            }

            response.Option = response.Option;

            _context.Responses.Update(response);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
