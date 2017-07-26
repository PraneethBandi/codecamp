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
        public List<PollResult> getPollData(string poll)
        {
            List<PollResult> results = new List<Controllers.PollResult>();
            foreach(var key in data.polldata.Keys)
            {
                results.Add(new PollResult()
                {
                    category = key,
                    count = data.polldata[key]
                });
            }
            return results;
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
        public string category;
        public int count;
    }
}
