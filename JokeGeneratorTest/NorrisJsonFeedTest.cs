using System;
using Xunit;
using JokeGenerator;
using System.Collections.Generic;

namespace JokeGeneratorTest
{
    public class NorrisJsonFeedTest
    {

        /// <summary>
        /// test to make sure JsonFeed.GetCategories() a list of categories.
        /// </summary>
        [Fact]
        public void GetCategories()
        {
            List<string> result = NorrisJsonFeed.GetCategories();

            string expected1 = "animal"; // we know some of the values, just testing that some o
            string expected2 = "career"; // just going to test

            Assert.Contains(expected1, result);
            Assert.Contains(expected2, result);
        }

        /// <summary>
        /// test to make sure JsonFeed.GetRandomJoke returns a joke.
        /// </summary>
        [Fact]
        public void GetRandomJokewCategory()
        {
            string result = NorrisJsonFeed.GetRandomJoke("animal");

            Assert.Contains("Chuck Norris", result);
        }


        /// <summary>
        /// test to make sure JsonFeed.GetRandomJoke returns a joke when no category passed in.
        /// </summary>
        [Fact]
        public void GetRandomJokeEmpty()
        {
            string result = NorrisJsonFeed.GetRandomJoke("");

            Assert.Contains("Chuck Norris", result);
        }

        /// <summary>
        /// test to make sure JsonFeed.GetRandomJokes returns several jokes.
        /// </summary>
        [Fact]
        public void GetRandomJokesCategory()
        {
            var result = NorrisJsonFeed.GetRandomJokes(10, "");

            Assert.True(result.Count == 10);
            foreach (var joke in result)
            {
                Assert.Contains("Chuck Norris", joke, StringComparison.CurrentCultureIgnoreCase);
            }

        }
    }
}
