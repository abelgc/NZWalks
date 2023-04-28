using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    //https://localhost:pornumber/api/students

    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMembersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllFamilyMembers()
        {
            string[]result = await Task.Run(() =>
            {
                // Perform some time-consuming operation
                System.Threading.Thread.Sleep(10000); // Simulate 2 seconds of work

                // Return the desired string result
                return new []  { "Abel", "Aleksandra", "Gheeta", "Baby Bean" };
            });

            return Ok(result);
        }

    }
}
