using System;
using Xunit;
using JokeGenerator;
using System.Collections.Generic;
using System.Linq;

namespace JokeGeneratorTest
{
    /// <summary>
    /// This is a class to override the JokeFlow to use a buffer set at testing
    /// </summary>
    class JokeFlowOverride : JokeFlow
    {
        /// <summary>
        /// input test buffer
        /// </summary>

        internal List<string> inputBuffer;
        /// <summary>
        /// output test buffer
        /// </summary>
        internal List<string> outputBuffer;

        /// <summary>
        /// overrides Input, redirecting to pull input from the input buffer
        /// </summary>
        /// <returns>the next string to be inputed</returns>
        public override string Input()
        {
            string retval = inputBuffer[0];
            inputBuffer.RemoveAt(0);
            return retval;
        }

        /// <summary>
        /// overrides InputChar, redirecting to pull input from the input buffer
        /// </summary>
        /// <returns>the next string to be inputed</returns>
        public override string InputChar()
        {
            return this.Input();
        } 

        /// <summary>
        /// Overrides the print method. This directs the print out to a buffer to be used for testing
        /// </summary>
        /// <param name="value">the string being added to the output buffer</param>
        public override void Print(string value)
        {
            outputBuffer.Add(value);
        }
    }

    /// <summary>
    /// tests the JokeFlow
    /// </summary>
    public class JokeFlowTest
    {
        /// <summary>
        /// Quick smoke test quits immediatly. Checks if feed start succesfully.
        /// </summary>
        [Fact]
        public void SmokeTest()
        {
            JokeFlowOverride jokeFlow = new JokeFlowOverride
            {

                // set the input buffer with the input that would be keyed in.
                inputBuffer = new List<string>(new string[] { "q" }),
                outputBuffer = new List<string>()
            };

            jokeFlow.Flow();

            // check that the output buffer has the correct data in it.
            Assert.Equal(Questions.Instructions, jokeFlow.outputBuffer[0]);
        }

        /// <summary>
        /// Check that pressing q quits the program
        /// </summary>
        [Fact]
        public void QForQuit()
        {
            JokeFlowOverride jokeFlow = new JokeFlowOverride
            {

                // set the input buffer with the input that would be keyed in.
                inputBuffer = new List<string>(new string[] { "q" }),
                outputBuffer = new List<string>()
            };

            jokeFlow.Flow();

            // check that the output buffer has the correct data in it.
            Assert.Equal(Questions.QuitText, jokeFlow.outputBuffer[1]);
        }

        /// <summary>
        /// Check that pressing c displays categories
        /// </summary>
        [Fact]
        public void CForCategories()
        {
            JokeFlowOverride jokeFlow = new JokeFlowOverride
            {

                // set the input buffer with the input that would be keyed in.
                inputBuffer = new List<string>(new string[] { "c", "q" }),
                outputBuffer = new List<string>()
            };

            jokeFlow.Flow();

            // check that the output buffer has the correct data in it.
            Assert.Equal(Questions.CategoryTitle, jokeFlow.outputBuffer[1]);
        }

        /// <summary>
        /// Check random joke path with:
        /// no category specified
        /// no random name
        /// 1 joke
        /// </summary>
        [Fact]
        public void RForRandomJokesPath1()
        {
            JokeFlowOverride jokeFlow = new JokeFlowOverride
            {

                // set the input buffer with the input that would be keyed in.
                inputBuffer = new List<string>(new string[] { "r", "n", "n", "1", "q" }),
                outputBuffer = new List<string>()
            };

            jokeFlow.Flow();

            // check that the output buffer has the correct data in it.
            Assert.Equal(Questions.Instructions, jokeFlow.outputBuffer[0]);
            Assert.Equal(Questions.WantToUseRandomName, jokeFlow.outputBuffer[1]);
            Assert.Equal(Questions.WantToSpecifyCategory, jokeFlow.outputBuffer[2]);
            Assert.Equal(Questions.HowManyJokes, jokeFlow.outputBuffer[3]);
            Assert.NotNull(jokeFlow.outputBuffer[4]);
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[4].ToLower());
            Assert.Equal(Questions.QuitText, jokeFlow.outputBuffer[5]);
        }

        /// <summary>
        /// Check random joke path with:
        /// category specified
        /// random name
        /// 1 joke
        /// </summary>
        [Fact]
        public void RForRandomJokesPath2()
        {
            JokeFlowOverride jokeFlow = new JokeFlowOverride
            {

                // set the input buffer with the input that would be keyed in.
                inputBuffer = new List<string>(new string[] { "r", "y", "y", "dev", "1", "q" }),
                outputBuffer = new List<string>()
            };

            jokeFlow.Flow();

            // check that the output buffer has the correct data in it.
            Assert.Equal(Questions.Instructions, jokeFlow.outputBuffer[0]);
            Assert.Equal(Questions.WantToUseRandomName, jokeFlow.outputBuffer[1]);
            Assert.Equal(Questions.WantToSpecifyCategory, jokeFlow.outputBuffer[2]);
            Assert.Equal(Questions.EnterCategory, jokeFlow.outputBuffer[3]);
            Assert.Equal(Questions.HowManyJokes, jokeFlow.outputBuffer[4]);
            Assert.NotNull(jokeFlow.outputBuffer[5]);
            Assert.DoesNotContain("chuck norris", jokeFlow.outputBuffer[5].ToLower());
            Assert.Equal(Questions.QuitText, jokeFlow.outputBuffer[6]);
        }

        /// <summary>
        /// Check random joke path with:
        /// no category specified
        /// no random name
        /// 1 joke
        /// </summary>
        [Fact]
        public void RForRandomJokesPath3()
        {
            JokeFlowOverride jokeFlow = new JokeFlowOverride
            {

                // set the input buffer with the input that would be keyed in.
                inputBuffer = new List<string>(new string[] { "r", "y", "y", "InvalidInvalid", "dev", "1", "q" }),
                outputBuffer = new List<string>()
            };

            jokeFlow.Flow();

            // check that the output buffer has the correct data in it.
            Assert.Equal(Questions.Instructions, jokeFlow.outputBuffer[0]);
            Assert.Equal(Questions.WantToUseRandomName, jokeFlow.outputBuffer[1]);
            Assert.Equal(Questions.WantToSpecifyCategory, jokeFlow.outputBuffer[2]);
            Assert.Equal(Questions.EnterCategory, jokeFlow.outputBuffer[3]);
            Assert.Equal(Questions.InvalidCategory.Replace("category", "InvalidInvalid"), jokeFlow.outputBuffer[4]);
        }

        /// <summary>
        /// Check random joke path with:
        /// no category specified
        /// no random name
        /// 9 jokes
        /// </summary>
        [Fact]
        public void RForRandomJokesPath4()
        {
            JokeFlowOverride jokeFlow = new JokeFlowOverride
            {

                // set the input buffer with the input that would be keyed in.
                inputBuffer = new List<string>(new string[] { "r", "n", "n", "9", "q" }),
                outputBuffer = new List<string>()
            };

            jokeFlow.Flow();

            // check that the output buffer has the correct data in it.
            Assert.Equal(Questions.Instructions, jokeFlow.outputBuffer[0]);
            Assert.Equal(Questions.WantToUseRandomName, jokeFlow.outputBuffer[1]);
            Assert.Equal(Questions.WantToSpecifyCategory, jokeFlow.outputBuffer[2]);
            Assert.Equal(Questions.HowManyJokes, jokeFlow.outputBuffer[3]);
            Assert.NotNull(jokeFlow.outputBuffer[4]);
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[4].ToLower());
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[5].ToLower());
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[6].ToLower());
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[7].ToLower());
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[8].ToLower());
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[9].ToLower());
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[10].ToLower());
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[11].ToLower());
            Assert.Contains("chuck norris", jokeFlow.outputBuffer[12].ToLower());

            Assert.Equal(Questions.QuitText, jokeFlow.outputBuffer[13]);
        }
    }
}
