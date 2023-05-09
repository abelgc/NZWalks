using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.V1.Controllers
{
    //https://localhost:pornumber/api/students

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FamilyMembersController : ControllerBase
    {
        //[HttpGet]
        //public async Task<IActionResult> GetAllFamilyMembers()
        //{
        //    string[]result = await Task.Run(() =>
        //    {
        //        // Perform some time-consuming operation
        //        System.Threading.Thread.Sleep(10000); // Simulate 2 seconds of work

        //        // Return the desired string result
        //        return new []  { "Abel", "Aleksandra", "Gheeta", "Baby Bean" };
        //    });

        //    return Ok(result);
        //}
        [MapToApiVersion("1.0")]
        [HttpGet]
        public IActionResult GetAllFamilyMembersApiVersionV1()
        {
            var members = FamilyMembersData.GetMembers();

            var response = new List<FamilyMembersDTOV1>();

            foreach (var member in members)
            {
                response.Add(new FamilyMembersDTOV1{Id = member.Id, Name = member.Name});
            }

            return Ok(response);
        }

    }
}
