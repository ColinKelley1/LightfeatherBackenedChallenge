using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LightfeatherBackendChallenge.Models;
using Microsoft.Extensions.Logging;

namespace LightfeatherBackendChallenge.Controllers
{
    [ApiController]
    public class SupervisorController : ControllerBase
    {
        private ILogger _logger;

        public SupervisorController(ILogger<SupervisorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("api/supervisors")]
        public async Task<IActionResult> GetSupervisorsAsync(string path)
        {
            HttpClient client = new HttpClient();
            string awsURL = $"https://o3m5qixdng.execute-api.us-east-1.amazonaws.com/api/managers"; 

            IEnumerable<Supervisor> managers = null;
            //System.Text.Json will not deserialize unless supplied this camelCase option. Alternative is Newtonsoft
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            HttpResponseMessage response = await client.GetAsync(awsURL);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Response successful. Parsing Json");
                managers = await JsonSerializer.DeserializeAsync<IEnumerable<Supervisor>>(await response.Content.ReadAsStreamAsync(), options);
            }
            else
            {
                _logger.LogError("Response unsuccessful. Returning Internal Server Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if(managers == null)
            {
                _logger.LogError("No managers pulled from URL.");
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var orderedManagers = managers.Where(m => !int.TryParse(m.Jurisdiction, out int throwAway))
                .OrderBy(m => m.Jurisdiction).ThenBy(m => m.LastName).ThenBy(m => m.FirstName);

            var managerReturn = orderedManagers.Select(m => string.Format("{0} - {1},{2}", m.Jurisdiction, m.LastName, m.FirstName)).ToList();

            return Ok(managerReturn);
        }

        [HttpPut]
        [Route("api/submit")]
        public IActionResult PutSupervisor(SupervisorSubmission supervisor)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
              
                Console.WriteLine("");
                Console.WriteLine("--PUT: Information recieved--");
                Console.WriteLine("Supervisor: " + supervisor.Supervisor);
                Console.WriteLine("First: " + supervisor.FirstName);
                Console.WriteLine("Last: " + supervisor.LastName);

                if (!string.IsNullOrEmpty(supervisor.PhoneNumber))
                    Console.WriteLine("Phone: " + supervisor.PhoneNumber);

                if (!string.IsNullOrEmpty(supervisor.Email))             
                    Console.WriteLine("Email: " + supervisor.Email);
                                
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }


    }
}
