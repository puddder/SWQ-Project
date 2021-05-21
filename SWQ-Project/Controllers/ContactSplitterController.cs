using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SWQ_Project.Models;
using SWQ_Project.Services;

namespace SWQ_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactSplitterController : ControllerBase
    {
        private readonly IContactSpillter _contactSpillter;
        
        public ContactSplitterController(IContactSpillter contactSpillter)
        {
            _contactSpillter = contactSpillter;
        }

        [HttpGet]
        public async Task<IActionResult> SplitContact([FromQuery] CompleteContactModel model)
        {
            SplitContact response;
            try
            {
                response = _contactSpillter.Split(model);
            }
            catch (Exception e)
            {
                return BadRequest("Please enter name in valid format");
            }
            
            if (response.Firstname is null
            && response.Lastname is null
            && response.Gender == Gender.Unknown
            && response.Language is null
            && response.Salutation is null
            && response.Title is null
            && response.LetterSalutation is null
            && response.SalutationTitle is null)
            {
                return BadRequest("Please enter name in valid format");
            }
            
            return Ok(response);
        }

        [HttpPost("title")]
        public async Task<IActionResult> Title(string title)
        {
            if (_contactSpillter.CreateTitle(title))
            {
                return Ok();
            }
            return BadRequest("Title is invalid or already exist.");
        }
        
        [HttpPost("salutation")]
        public async Task<IActionResult> Salutation(SalutationModel model)
        {
            if (_contactSpillter.CreateSalutation(model))
            {
                return Ok();
            }
            return BadRequest("Salutation is invalid or already exist.");
        }
    }
}