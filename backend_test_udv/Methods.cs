using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace backend_test_udv.Controllers
{
    public static class Methods
    {
        public static async Task<List<Item>> GetTextFromPostsByReqest(string request)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<Rootobject>(responseBody);
            var list = new List<Item>();
            if (post.response == null) return null;        
            foreach (var elem in post.response.items)
            {                
                list.Add(elem);
            }
            return list;
        }

        public static Dictionary<char, int> GetUniqueChars(string s, Dictionary<char, int> previous)
        {
            var dict = previous;
            foreach (var elem in s)
            {
                if (char.IsLetter(elem) || char.IsDigit(elem))
                {
                    if (dict.ContainsKey(char.ToUpper(elem))) dict[char.ToUpper(elem)] += 1;
                    else dict.Add(char.ToUpper(elem), 1);
                }
            }
            return dict;
        }
    }
}
