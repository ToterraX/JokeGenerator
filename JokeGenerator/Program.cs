using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JokeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var jokeFlow = new JokeFlow();

            jokeFlow.Flow();
        }
    }
}
