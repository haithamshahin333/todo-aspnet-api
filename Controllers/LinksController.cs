using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public LinksController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("dependency")]
        public async Task<ActionResult> GetJsonData()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
    "https://raw.githubusercontent.com/haithamshahin333/json-test-data/main/testdata.json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return Ok(response.Content.ReadAsStream());
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpGet("503failure")]
        public ActionResult Get503Failure()
        {
            return StatusCode(503);
        }

        [HttpGet("Sleep")]
        public ActionResult GetSleep()
        {
            Thread.Sleep(5000);
            return Ok("Awake!");
        }

    }
}
