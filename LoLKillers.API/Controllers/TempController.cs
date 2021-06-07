using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoLKillers.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempController : ControllerBase
    {
        // GET: api/<TempController>
        [HttpGet]
        public string Get()
        {
            var url = "https://ddragon.leagueoflegends.com/api/versions.json";
            string version = "";

            using (var httpClient = new HttpClient())
            {
                var x = httpClient.GetAsync(url).Result;
                var y = x.Content.ReadAsStringAsync().Result;
                var stuff = JsonConvert.DeserializeObject<List<string>>(y);
                version = stuff[0];
            }

            //return new string[] { "value1", "value2" };
            return version;
        }

        // GET api/<TempController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TempController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TempController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TempController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
