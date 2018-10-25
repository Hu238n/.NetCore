using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/a")]
    public class ValuesController : Controller
    {

        string url = "https://jsonplaceholder.typicode.com/posts";
        List<Comment> comment = new List<Comment>();
        List<Posts> Post = new List<Posts>();
              [HttpGet]
        [Route("post")]
        public  ActionResult Index()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                MediaTypeWithQualityHeaderValue contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                HttpResponseMessage Res = client.GetAsync(url).Result;
                string stringData = Res.Content.ReadAsStringAsync().Result;
                Post = JsonConvert.DeserializeObject<List<Posts>>(stringData);

                using (HttpClient client1 = new HttpClient())
                {
                      client1.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/comments");
                      MediaTypeWithQualityHeaderValue ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                      client.DefaultRequestHeaders.Accept.Add(ContentType);
                      HttpResponseMessage Response = client1.GetAsync("https://jsonplaceholder.typicode.com/comments").Result;
                      String strData = Response.Content.ReadAsStringAsync().Result;
                      comment = JsonConvert.DeserializeObject<List<Comment>>(strData);
                }
                var result =  Post.Select(x => new
                {
                 Post=x.body,
                    comment = comment.Where(d => d.postId == x.userId
            ).Select(
                        r => new
                        {
                            Name= r.name,
                            Email= r.email,
                            comment=r.body,
                        }
                ), 
                });
                return Ok(result);
            }
        }

       // [HttpGet]
        //public ActionResult GetComment()
        //{
        //    using (
        //        HttpClient client = new HttpClient())
        //    {
                
        //    }
        //}
    }  
}
