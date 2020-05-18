using System;
using Xunit;
using JokeGenerator;
using System.Collections.Generic;

namespace JokeGeneratorTest
{
    public class PrivServJsonFeedTest
    {
        /// <summary>
        /// test to make sure JsonFeed.GetName() returns a name and a surname.
        /// </summary>
        [Fact]
        public void GetName()
        {
            var result = PrivServJsonFeed.GetName();

            Assert.True(result.FirstName.Length > 0);
            Assert.True(result.LastName.Length > 0);
        }
    }
}
