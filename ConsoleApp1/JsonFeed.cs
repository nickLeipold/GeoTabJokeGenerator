using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class JsonFeed
    {
        static string _url = "";

        public JsonFeed() { }
        public JsonFeed(string endpoint)
        {
            _url = endpoint;
        }
        
		public static string GetRandomJoke(string category)
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			string url = "jokes/random";
			if (category != null && !string.Equals(category, ""))
			{
				url += "?";
				url += "category=";
				url += category;
			}
			
			dynamic response = JsonConvert.DeserializeObject<dynamic>(Task.FromResult(client.GetStringAsync(url).Result).Result);

            return response.value;

        }

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public static Tuple<String,String> Getnames()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			string url = "api/";
			var result = client.GetStringAsync(url).Result;
			Console.WriteLine(result);
			dynamic response = JsonConvert.DeserializeObject<dynamic>(result);
			// Console.WriteLine(response.SelectToken("name"));
			// Console.WriteLine(typeof response.SelectToken("name"));
			// Console.WriteLine(response.surname);

			Tuple<String, String> names = new Tuple<string, string>(response.SelectToken("name").ToString(), response.SelectToken("surname").ToString());
			return names;
		}

		public static string[] GetCategories()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);

			string url = "jokes/categories";

			var result = client.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<string[]>(result);
		}
    }
}
