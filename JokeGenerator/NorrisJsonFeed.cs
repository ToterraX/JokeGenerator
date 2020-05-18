using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JokeGenerator.Properties;
using Newtonsoft.Json;

namespace JokeGenerator
{
	/// <summary>
	/// Handles the communication with the Chuck Norris JSON API
	/// </summary>
    public static class NorrisJsonFeed
    {
		private static List<string> categories;

		/// <summary>
		/// Gets a single joke from the NorrisAPI
		/// </summary>
		/// <param name="category">The category passed in</param>
		/// <returns>a string containing the joke</returns>
		public static string GetRandomJoke(string category)
		{
			HttpClient client = new HttpClient();
			var uri = new UriBuilder(Resources.ChuckNorrisApi +Resources.RandomNamePath);
			
			if (category != null && category != string.Empty)
			{
				var paramValues = HttpUtility.ParseQueryString(uri.ToString());
				paramValues.Add("category", category);

				uri.Query = paramValues.ToString();
			}

			string result = Task.FromResult(client.GetStringAsync(uri.Uri).Result).Result;

			return JsonConvert.DeserializeObject<dynamic>(result).value;
		}

		/// <summary>
		/// Gets a multiple jokes from the NorrisAPI
		/// </summary>
		/// <param name="numberOfJokes">The number of jokes to return</param>
		/// <param name="category">The category passed in</param>
		/// <returns>a string containing the joke</returns>
		public static List<string> GetRandomJokes(int numberOfJokes, string category)
		{
			var jokes = new List<string>();
			var loopCounter = 0;

			while (jokes.Count < numberOfJokes && loopCounter < 20)
			{
				var newJoke = GetRandomJoke(category);

				if (!jokes.Contains(newJoke))
				{
					jokes.Add(newJoke);
				}

				loopCounter++;
			}

			return jokes;
		}

		/// <summary>
		///  Fetches from the Chuck Norris API a list of categories.
		/// </summary>
		/// <returns>String of category names</returns>
		public static List<string> GetCategories()
		{
			if (categories == null)
			{
				HttpClient client = new HttpClient
				{
					BaseAddress = new Uri(Resources.ChuckNorrisApi)
				};

				var result = Task.FromResult(client.GetStringAsync(Resources.CategoriesPath).Result).Result;

				categories = JsonConvert.DeserializeObject<List<string>>(result);
			}
			
			return categories;
		}
    }
}
