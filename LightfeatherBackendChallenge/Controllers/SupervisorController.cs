﻿using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [Route("api/supervisors")]
        public async Task<IActionResult> GetSupervisorsAsync(string path)
        {
            HttpClient client = new HttpClient();
            string awsURL = $"https://o3m5qixdng.execute-api.us-east-1.amazonaws.com/api/managers"; 
            HttpResponseMessage response = await client.GetAsync(awsURL);

            IEnumerable<Supervisor> managers = null;
            //System.Text.Json will not deserialize unless supplied this camelCase option. Alternative is Newtonsoft
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            if (response.IsSuccessStatusCode)
            {
                managers = await JsonSerializer.DeserializeAsync<IEnumerable<Supervisor>>(await response.Content.ReadAsStreamAsync(), options);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if(managers == null)
            {
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
                return BadRequest(e.Message);
            }
        }


    }
}
