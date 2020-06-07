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
			if (category != null)
			{
				url += "?";
				url += "category=";
				url += category;
			}
			


            return Task.FromResult(client.GetStringAsync(url).Result).Result;

        }

        /// <summary>
        /// returns an object that contains name and surname
        /// </summary>
        /// <param name="client2"></param>
        /// <returns></returns>
		public static dynamic Getnames()
		{
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(_url);
			string url = "api/";
			var result = client.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<dynamic>(result);
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
