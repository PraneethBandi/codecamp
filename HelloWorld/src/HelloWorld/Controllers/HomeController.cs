using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
            Response.Cookies.Append("submitted","false");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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
            await httpClient.GetAsync(string.Format("{0}/{1}",serviceUrl,data.poll));
            return Json(new { data = data.poll});
        }
    }

    public class input
    {
        public string poll;
    }
}
