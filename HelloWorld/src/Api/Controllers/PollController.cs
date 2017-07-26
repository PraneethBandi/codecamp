using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class PollController : Controller
    {
        private static Dictionary<string, int> polls = new Dictionary<string, int>();
        
        [HttpGet("results")]
        public ActionResult Results()
        {
            List<PollResult> results = new List<Controllers.PollResult>();
            polls.Keys.All(k =>
            {
                results.Add(new PollResult()
                {
                    choice = k,
                    total = polls[k]
                });
                return true;
            });

            return new JsonResult(results);
        }

        [HttpGet("vote/{choice}")]
        public bool Vote([FromRoute] string choice)
        {
            if (!string.IsNullOrEmpty(choice))
            {
                choice = choice.ToLower();
                if (polls.ContainsKey(choice))
                {
                    polls[choice]++;
                }
                else
                {
                    polls[choice] = 1;
                }
                return true;
            }

            return false;
        }        
    }

    public class PollResult
    {
        public string choice = string.Empty;
        public int total = 0;
    }
}
