using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;
        public HomeController(HttpClient hClient){
            httpClient = hClient;
        }
        public IActionResult Index()
        {
            //Response.Cookies.Append("submitted","false");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Results()
        {
            var serviceUrl = Environment.GetEnvironmentVariable("cacheservice");
            var response = await httpClient.GetAsync(string.Format("{0}/{1}", serviceUrl, "getpollresults"));

            var data = await response.Content.ReadAsStringAsync();
            object content = JsonConvert.DeserializeObject(data);
            
            float yes = Convert.ToInt32(((Newtonsoft.Json.Linq.JObject)(content)).GetValue("yes").ToString());
            float no = Convert.ToInt32(((Newtonsoft.Json.Linq.JObject)(content)).GetValue("no").ToString());

            ViewData["YesPercent"] = (yes / (yes + no)) * 100;
            ViewData["NoPercent"] = (no / (yes + no)) * 100;
            ViewData["total"] = Convert.ToInt32(yes + no);
            return View("Results");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> PollSubmit([FromBody] input data)
        {
            if(Request.Cookies.ContainsKey("submitted") && Request.Cookies["submitted"] == "true"){
                return Json(new { data = "NOOOOOOOOOOOOOO DUPES!!!!!"});
            }

            Response.Cookies.Append("submitted","true");
            var serviceUrl = Environment.GetEnvironmentVariable("cacheservice");
            await httpClient.GetAsync(string.Format("{0}/{1}/{2}",serviceUrl, "submitpoll", data.poll));
            return Json(new { data = data.poll});
        }
    }

    public class input
    {
        public string poll;
    }
}
