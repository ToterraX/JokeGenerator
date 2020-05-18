using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JokeGenerator.Properties;
using Newtonsoft.Json;

namespace JokeGenerator
{
    /// <summary>
    /// Handles the communication with the privServ JSON API (used to create random people)
    /// </summary>
    public static class PrivServJsonFeed
    {
        /// <summary>
        /// represents a person returned from the getName function
        /// </summary>
        public struct Person
        {
            /// <summary>
            /// First Name
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// Last Name
            /// </summary>
            public string LastName { get; set; }
        }

        /// <summary>
        /// Fetches from the PrivServ API a randomly made up person.
        /// </summary>
        /// <returns>The dictionary object containing information about the person</returns>
        public static Person GetName()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Resources.PrivServApi)
            };
            var result = client.GetStringAsync("").Result;

            var person = new Person
            {
                FirstName = (JsonConvert.DeserializeObject<dynamic>(result)).name
                ,
                LastName = (JsonConvert.DeserializeObject<dynamic>(result)).surname
            };

            return person;
        }
    }
}
