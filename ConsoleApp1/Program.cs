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
        static string[] results = new string[50];

        static ConsolePrinter printer = new ConsolePrinter(true);

        static void Main(string[] args)
        {
            Boolean instructionsOn;
            Tuple<string, string> names = null;
            //checks to see if we are in api mode which doesnt need to output menus
            if (args.Length == 1 && string.Equals(args[0], "--api"))
            {
                instructionsOn = false;
                printer = new ConsolePrinter(false);
            }
            else
            {
                instructionsOn = true;
                printer.Value("Welcome to the joke generator, below are options to generate jokes").ToString();
            }


            while (true)
            {
                if (instructionsOn)
                {
                    //Default menu here
                    printer.Value("Press c to get categories").ToString();
                    printer.Value("Press r to get random jokes").ToString();
                    printer.Value("Press q to exit program").ToString();
                }

                char key = GetEnteredKey(Console.ReadLine());

                if (key == 'c')
                {
                    results = getCategories();
                    PrintResults(results, 2);
                }
                else if (key == 'r')
                {
                    printer.Value("Want to use a random name? y/n").ToString();
                    bool decision = detectBooleanResponse(GetEnteredKey(Console.ReadLine()));
                    string category = "";

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

                    printer.Value("How many jokes do you want? (1-9)").ToString();
                    int n = Int32.Parse(Console.ReadLine());
                    results = GetRandomJokes(category, n, names);
                    printer.Value("Here are your jokes you asked for:").ToString();
                    PrintResults(results, 2, !instructionsOn);

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


        private static string selectCategory()
        {
            string category;

            printer.Value("Here are the available categories:").ToString();
            results = getCategories();
            PrintResults(results, 2);
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
            if(force){
                indent = 0;
            }
            for (int i = 0; i < indent; i++)
            {
                spaces += " ";
            }
            for (int i = 0; i < results.Length; i++)
            {
                printer.Value(spaces + results[i]).ToStringForce();
            }
        }

        private static char GetEnteredKey(String input)
        {
            switch (input)
            {
                case "c":
                    return 'c';
                case "0":
                    return '0';
                case "1":
                    return '1';
                case "2":
                    return '2';
                case "3":
                    return '3';
                case "4":
                    return '4';
                case "5":
                    return '5';
                case "6":
                    return '6';
                case "7":
                    return '7';
                case "8":
                    return '8';
                case "9":
                    return '9';
                case "r":
                    return 'r';
                case "y":
                    return 'y';
                case "q":
                    return 'q';
                case "?":
                    return '?';
                default:
                    return '!';
            }
        }

        private static String[] GetRandomJokes(string category, int number, Tuple<String, String> names = null)
        {
            if (names == null)
            {
                names = new Tuple<String, String>(null, null);
            }
            string[] jokes = new string[number]; //hard coded max, should be made dynamic if time allows
            new JsonFeed("https://api.chucknorris.io/");
            // Console.WriteLine("category: '" + category+"'");
            for (int i = 0; i < number; i++)
            {
                jokes[i] = JsonFeed.GetRandomJoke(category);
            }
            // Console.WriteLine()
            for (int i = 0; i < number; i++)
            {

                if (names.Item1 != null)
                {
                    string firstname = names.Item1;
                    string lastname = names.Item2;
                    jokes[i] = replaceName("Chuck Norris", firstname + " " + lastname, jokes[i]);
                    
                }
            }
            return jokes;
        }

        private static string replaceName(string oldVale, string newValue, string words) {
            //will check for a plural of Chuck Norris and replace ith the appropriate plural of the new name
            if(newValue.Last() == 's') {
                words = words.Replace("Chuck Norris'", newValue + "'");
            }else {
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
