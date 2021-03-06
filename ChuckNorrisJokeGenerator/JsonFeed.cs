﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChuckNorrisJokeGenerator
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
            try
            {
                new JsonFeed("https://api.chucknorris.io/");
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
            catch (System.Exception)
            {
				Console.WriteLine("Error: the conncetion to the random joke server has failed");
                throw;
            }

        }

        /// returns an object that contains name and surname
		public static Tuple<String, String> Getnames()
        {
            try
            {
                new JsonFeed("https://names.privserv.com/");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_url);
                string url = "api/";
                var result = client.GetStringAsync(url).Result;
                dynamic response = JsonConvert.DeserializeObject<dynamic>(result);

                Tuple<String, String> names = new Tuple<string, string>(response.SelectToken("name").ToString(), response.SelectToken("surname").ToString());
                return names;

            }
            catch (System.Exception)
            {
                Console.WriteLine("Error: the connection to the names server has failed");
                throw;
            }
        }

        public static string[] GetCategories()
        {
            try
            {
                new JsonFeed("https://api.chucknorris.io/");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_url);

                string url = "jokes/categories";

                var result = client.GetStringAsync(url).Result;
                return JsonConvert.DeserializeObject<string[]>(result);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Error: the connection to the categories server has failed");
                throw;
            }

        }
    }
}
