using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SurveyApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly SurveyContext _context;

        public SurveyController(SurveyContext context)
        {
            _context = context;

            if (_context.Surveys.Count() == 0)
            {
                // Create a new Survey if collection is empty,
                // which means you can't delete all Surveys.
                _context.Surveys.Add(new Survey { Question = "Question1" });
                _context.SaveChanges();
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public ActionResult<List<Survey>> GetAll()
        {
            return _context.Surveys.ToList();
        }

        // GET: api/<controller>/5
        [HttpGet("{id}", Name = "GetSurvey")]
        public ActionResult<Survey> GetById(int id)
        {
            var survey = _context.Surveys.Find(id);
            if (survey == null)
            {
                return NotFound();
            }
            return survey;
        }

        // POST: api/<controller>
        [HttpPost]
        public IActionResult Create(Survey survey)
        {
            var choice = new Choice
            {
                Option = survey.Choice.Option,
                Survey = survey
            };

            _context.Choices.Add(choice);
            _context.Surveys.Add(survey);
            _context.SaveChanges();

            return CreatedAtRoute("GetSurvey", new { id = survey.Id }, survey);
        }
        
        // PUT: api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Survey survey)
        {
            var question = _context.Surveys.Find(id);
            if (question == null)
            {
                return NotFound();
            }
            
            _context.Surveys.Update(question);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Survey survey)
        {
            _context.Entry(survey).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var survey1 = _context.Surveys.Find(id);
            if (survey1 == null)
            {
                return NotFound();
            }

            _context.Surveys.Remove(survey1);
            _context.SaveChanges();

            return NoContent();
        }

        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
