using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;

namespace backend_test_udv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VKPostController : ControllerBase
    {
        [HttpGet]
        [Route("GetWallPosts")]
        public IActionResult GetPosts(int id = 199373173, int count = 5)
        {
            if (count < 0) return BadRequest("ВВедите неотрицательное количество последних постов");
            var dictionary = new Dictionary<char, int>();
            var PARAMS = $"id={id}&count={count}";
            var request = $"https://api.vk.com/method/wall.get?{PARAMS}&access_token={Startup.vkApiToken}&v=5.131";
            try
            {
                var itemsFromPosts = Methods.GetTextFromPostsByReqest(request).Result.ToList();
            }
            catch
            {
                return BadRequest("Пользователь не найден или профиль скрыт");
            }
            var itemFromPosts = Methods.GetTextFromPostsByReqest(request).Result.ToList();
            try
            {
                if (itemFromPosts.First().owner_id != id) return BadRequest("Пользователь не найден или профиль скрыт");
            }
            catch { }
            foreach (var elem in itemFromPosts)
            {
                dictionary = Methods.GetUniqueChars(elem.text, dictionary);
            }
            var post = new Postshistory();
            post.Datetime = DateTime.Now.ToString();
            post.Dictresult = JsonConvert.SerializeObject(dictionary);
            var context = new TestDBContext();
            context.Add(post);
            context.SaveChanges();
            return Ok(dictionary);
        }

        [HttpGet]
        [Route("GetPostsHistory")]
        public IEnumerable<Postshistory> GetPostsHistory()
        {
            var context = new TestDBContext();
            foreach(var elem in context.Postshistories) yield return elem;
        }
    }   
}
