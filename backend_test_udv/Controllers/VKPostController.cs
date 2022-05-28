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
        public Dictionary<char,int> GetPosts(int id = 199373173, int count = 5)
        {
            var dictionary = new Dictionary<char, int>();
            var PARAMS = $"id={id}&count={count}";
            var request = $"https://api.vk.com/method/wall.get?{PARAMS}&access_token={Startup.vkApiToken}&v=5.131";
            var textFromPosts = Methods.GetTextFromPostsByReqest(request).Result.ToList();
            foreach(var elem in textFromPosts)
            {
                dictionary = Methods.GetUniqueChars(elem, dictionary);
            }
            var post = new Postshistory();
            post.Datetime = DateTime.Now.ToString();
            post.Dictresult = JsonConvert.SerializeObject(dictionary);
            var context = new TestDBContext();
            context.Add(post);
            context.SaveChanges();
            return dictionary;
        }

        [HttpGet]
        [Route("GetPostsHistory")]
        public IEnumerable<Postshistory> GetPostsHistory()
        {
            var context = new TestDBContext();
            foreach(var elem in context.Postshistories) yield return elem;
        }
    }

    public static class Methods
    {
        public static async Task<List<string>> GetTextFromPostsByReqest(string request)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<Rootobject>(responseBody);
            var list = new List<string>();
            foreach (var elem in post.response.items)
            {
                list.Add(elem.text);
            }
            return list;
        }

        public static Dictionary<char, int> GetUniqueChars(string s, Dictionary<char, int> previous)
        {
            var dict = previous;
            foreach(var elem in s)
            {
                if(char.IsLetter(elem) || char.IsDigit(elem))
                {
                    if (dict.ContainsKey(char.ToUpper(elem))) dict[char.ToUpper(elem)] += 1;
                    else dict.Add(char.ToUpper(elem), 1);
                }
            }
            return dict;
        }
    }
}
