using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static ConsolePrinter printer = new ConsolePrinter(true);

        static void Main(string[] args)
        {
            Boolean instructionsOn;
            //checks to see if we are in api mode which doesnt need to output menus
            if (args.Length == 1 && string.Equals(args[0], "--api"))
            {
                instructionsOn = false;
                printer = new ConsolePrinter(false);
            }
            else
            {
                instructionsOn = true;
                printer.Value("Welcome to the Chuck Norris joke generator, below are options to generate jokes").ToString();
            }

            //execution loop
            while (true)
            {
                if (instructionsOn)
                {
                    printMainMenuOptions();
                }

                char key = GetEnteredKey(Console.ReadLine());

                if (key == 'c')
                {
                    printAvailableCategories();
                }
                else if (key == 'r')
                {
                    buildRandomJokes(instructionsOn);

                }
                else if (key == 'q')
                {
                    break;
                }
                else
                {
                    printer.Value("Invalid option selected").ToString();
                }
            }


        }

        private static void printMainMenuOptions()
        {
            //Default menu here
            printer.Value("Press c to get categories").ToString();
            printer.Value("Press r to get random jokes").ToString();
            printer.Value("Press q to exit program").ToString();
        }

        private static void buildRandomJokes(bool instructionsOn)
        {
            Tuple<string, string> names = null;
            string[] results = null;
            string category = "";
            printer.Value("Want to use a random name? y/n").ToString();
            bool decision = detectBooleanResponse(GetEnteredKey(Console.ReadLine()));
            if (decision)
            {
                names = GetNames();
            }

            printer.Value("Want to specify a category? y/n").ToString();
            decision = detectBooleanResponse(GetEnteredKey(Console.ReadLine()));
            if (decision)
            {
                category = selectCategory();
            }

            printer.Value("How many jokes do you want? (1-20)").ToString();
            int n = detectNumberResponse(Console.ReadLine());

            results = GetRandomJokes(category, n, names);
            printer.Value("Here are your jokes you asked for:").ToString();
            PrintResults(results, 2, !instructionsOn);
        }


        private static string selectCategory()
        {
            string category;
            string[] results;
            results = printAvailableCategories();
            category = Console.ReadLine();
            if (results.Contains(category.ToLower()))
            {
                return category.ToLower();
            }
            else
            {
                //will recursively call itself if the input category is not available.
                printer.Value("The category '" + category + "' is not a valid option, please choose again").ToString();
                return selectCategory();
            }
        }

        private static string[] printAvailableCategories()
        {
            string[] results;
            printer.Value("Here are the available categories:").ToString();
            results = getCategories();
            PrintResults(results, 2);
            return results;
        }

        private static int detectNumberResponse(string response)
        {
            try
            {
                int number = Int32.Parse(response);
                if (validRange(number))
                {
                    return number;
                }
                printer.Value("Invalid number of jokes, choose a number between 1 and 20").ToString();
                return detectNumberResponse(Console.ReadLine());
            }
            catch
            {
                printer.Value("Invalid number of jokes, choose a number between 1 and 20").ToString();
                return detectNumberResponse(Console.ReadLine());
            }

        }

        private static bool validRange(int number)
        {
            if (number > 0 && number <= 20)
            {
                return true;
            }
            return false;
        }
        private static bool detectBooleanResponse(char key)
        {
            if (key == 'y')
            {
                return true;
            }
            return false;
        }

        private static void PrintResults(string[] results, int indent = 0, bool force = false)
        {
            string spaces = "";
            if (force)
            {
                indent = 0;
            }
            for (int i = 0; i < indent; i++)
            {
                spaces += " ";
            }
            for (int i = 0; i < results.Length; i++)
            {
                if (force)
                {
                    printer.Value(spaces + results[i]).ToStringForce();
                }
                else
                {
                    printer.Value(spaces + results[i]).ToString();
                }
            }
        }

        private static char GetEnteredKey(String input)
        {
            switch (input)
            {
                case "c":
                    return 'c';
                case "r":
                    return 'r';
                case "y":
                    return 'y';
                case "q":
                    return 'q';
                case "n":
                    return 'n';
                default:
                    printer.Value("Invalid option, please try again").ToString();
                    return GetEnteredKey(Console.ReadLine());
            }
        }

        private static String[] GetRandomJokes(string category, int number, Tuple<String, String> names = null)
        {
            //if no names were passed in default to null strings
            if (names == null)
            {
                names = new Tuple<String, String>(null, null);
            }
            string[] jokes = new string[number];
            new JsonFeed("https://api.chucknorris.io/");
            for (int i = 0; i < number; i++) //loop through and get jokes
            {
                jokes[i] = JsonFeed.GetRandomJoke(category);
            }
            //loop through the jokes and replace names if names are available
            if (names.Item1 != null)
            {
                for (int i = 0; i < number; i++)
                {
                    string firstname = names.Item1;
                    string lastname = names.Item2;
                    jokes[i] = replaceName("Chuck Norris", firstname + " " + lastname, jokes[i]);

                }
            }
            return jokes;
        }

        private static string replaceName(string oldVale, string newValue, string words)
        {
            //will check for a plural of Chuck Norris and replace ith the appropriate plural of the new name
            if (newValue.Last() == 's')
            {
                words = words.Replace("Chuck Norris'", newValue + "'");
            }
            else
            {
                words = words.Replace("Chuck Norris'", newValue + "'s");
            }
            return words.Replace("Chuck Norris", newValue);
        }
        private static String[] getCategories()
        {
            new JsonFeed("https://api.chucknorris.io/");
            return JsonFeed.GetCategories();
        }

        private static Tuple<String, String> GetNames()
        {
            new JsonFeed("https://names.privserv.com/");
            return JsonFeed.Getnames();
        }
    }
}
