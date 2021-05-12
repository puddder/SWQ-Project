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
            var response = _contactSpillter.Split(model);
            return Ok(response);
        }

        [HttpPost("title")]
        public async Task<IActionResult> Title(string title)
        {
            if (_contactSpillter.CreateTitle(title))
            {
                return Ok();
            }
            return BadRequest("Title already exist.");
        }
        
        [HttpPost("salutation")]
        public async Task<IActionResult> Salutation(SalutationModel model)
        {
            if (_contactSpillter.CreateSalutation(model))
            {
                return Ok();
            }
            return BadRequest("Salutation already exist.");
        }
    }
}