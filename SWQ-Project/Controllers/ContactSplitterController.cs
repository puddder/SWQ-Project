using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> SplitContact()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Title()
        {
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> Salutation()
        {
            return Ok();
        }
    }
}