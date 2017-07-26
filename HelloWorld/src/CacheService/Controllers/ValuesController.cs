using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CacheService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        PollData data;
        public ValuesController(PollData polldata)
        {
            data = polldata;
        }
        // GET api/values
        [HttpGet("submitpoll/{poll}")]
        public bool Get(string poll)
        {
            if (data.polldata.ContainsKey(poll))
            {
                int count = data.polldata[poll];
                data.polldata[poll] = count + 1;
            }
            else
            {
                data.polldata[poll] = 1;
            }

            return true;
        }

        [HttpGet("getpollresults")]
        public PollResult getPollData(string poll)
        {
            PollResult result = new PollResult()
            {
                yes = 0,
                no = 0
            };

            try
            {
                if (data.polldata.ContainsKey("YES"))
                {
                    result.yes = data.polldata["YES"];
                }

                if (data.polldata.ContainsKey("NO"))
                {
                    result.no = data.polldata["NO"];
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class PollResult
    {
        public int yes;
        public int no;
    }
}
