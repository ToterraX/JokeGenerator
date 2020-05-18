using JokeGenerator.Properties;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml;

namespace JokeGenerator
{
    /// <summary>
    /// This class handles the 
    /// </summary>
    public class JokeFlow
    {
        /// <summary>
        /// Creates an instance of the JokeFlow
        /// </summary>
        public JokeFlow() { }

        /// <summary>
        /// the main question and answer flow for the program
        /// </summary>
        public void Flow()
        {
            Print(Questions.Instructions);
            while (true)
            {
                try
                {
                    var mainInput = InputChar();

                    switch (mainInput)
                    {
                        case "?":
                            PrintInstructions();
                            break;
                        case "c":
                            PrintCategories();
                            break;
                        case "r":
                            PrintRandomJokes();
                            break;
                        case "q":
                            PrintQuit();
                            return;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    Print(Questions.UnexpectedError);
                }
            }
        } 

        /// <summary>
        /// Reads input. Normally this is from the console but may be overriden.
        /// </summary>
        /// <returns>field inputed</returns>
        public virtual string Input()
        {
            Console.Write(Questions.PromptText);
            var returnValue = Console.ReadLine();
            Console.WriteLine();

            return returnValue;
        }

        /// <summary>
        /// Reads input. Normally this is from the console but may be overriden.
        /// </summary>
        /// <returns>field inputed</returns>
        public virtual string InputChar()
        {
            Console.Write(Questions.PromptText);
            var returnValue = Console.ReadKey().KeyChar.ToString();
            Console.WriteLine();
            Console.WriteLine();
            return returnValue;
        }

        /// <summary>
        /// Pr3ints output. Normally this is to the console but may be overriden.
        /// </summary>
        /// <param name="value">Value to output</param>
        public virtual void Print(string value)
        {
            Console.WriteLine(value);
        }

        private void PrintQuit()
        {
            Print(Questions.QuitText);
        }

        private void PrintRandomJokes()
        {
            PrivServJsonFeed.Person person = AskRandomName();
            string category = AskCategory();
            int numberJokes = AskNumberJokes();

            PrintJokes(category, person, numberJokes);
        }

        private void PrintJokes(string category, PrivServJsonFeed.Person person, int numberJokes)
        {
            foreach (var item in NorrisJsonFeed.GetRandomJokes(numberJokes, category))
            {
                string newItem = item.Replace(Resources.Chuck, person.FirstName, StringComparison.CurrentCultureIgnoreCase);
                newItem = newItem.Replace(Resources.Norris, person.LastName, StringComparison.CurrentCultureIgnoreCase);
                Print(newItem);
            }
        }

        private int AskNumberJokes()
        {
            while (true)
            {
                Print(Questions.HowManyJokes);

                if (int.TryParse(InputChar(), out int numberJokes) && numberJokes > 0 && numberJokes < 10)
                {
                    return numberJokes;
                }
                else
                {
                    Print(Questions.InvalidNumber.Replace("key", numberJokes.ToString()));
                }
            }
        }

        private string AskCategory()
        {
            string category;
            Print(Questions.WantToSpecifyCategory);
            if (InputChar().ToLower() == "y")
            {
                category = SpecifyCategory();
            }
            else
            {
                category = string.Empty;
            }

            return category;
        }

        private string SpecifyCategory()
        {
            while (true)
            {
                Print(Questions.EnterCategory);

                string category = Input();

                if (NorrisJsonFeed.GetCategories().Contains(category))
                {
                    return category;
                }
                else
                {
                    Print(Questions.InvalidCategory.Replace("category", category));
                    PrintCategories();
                }
            }
        }

        private PrivServJsonFeed.Person AskRandomName()
        {
            PrivServJsonFeed.Person person;

            Print(Questions.WantToUseRandomName);
            if (InputChar().ToLower() == "y")
            {
                person = PrivServJsonFeed.GetName();
            }
            else
            {
                person = new PrivServJsonFeed.Person { FirstName = Resources.Chuck, LastName = Resources.Norris };
            }

            return person;
        }

        private void PrintCategories()
        {
            Print(Questions.CategoryTitle);
            foreach (var item in NorrisJsonFeed.GetCategories())
            {
                Print(item);
            }
            Print(string.Empty);
        }

        private void PrintInstructions()
        {
            Print(Questions.Instructions);
        }
    }
}
