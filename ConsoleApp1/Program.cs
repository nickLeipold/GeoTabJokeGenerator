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
        // static char key;
        static Tuple<string, string> names;

        static ConsolePrinter printer = new ConsolePrinter();

        static void Main(string[] args)
        {
            Boolean instructionsOn;
            //checks to see if we are in api mode which doesnt need to output menus
            Console.WriteLine(args.Length + " : number of args");
            if (args.Length == 1 && string.Equals(args[0], "--api"))
            {
                instructionsOn = false;
            }else {
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
                    PrintResults(results);
                }
                else if (key == 'r')
                {
                    printer.Value("Want to use a random name? y/n").ToString();
                    key = GetEnteredKey(Console.ReadLine());
                    if (key == 'y')
                        names = GetNames();
                    printer.Value("Want to specify a category? y/n").ToString();
                    if (key == 'y')
                    {
                        printer.Value("How many jokes do you want? (1-9)").ToString();
                        int n = Int32.Parse(Console.ReadLine());
                        printer.Value("Enter a category;").ToString();
                        results = GetRandomJokes(Console.ReadLine(), n);
                        PrintResults(results);
                    }
                    else
                    {
                        printer.Value("How many jokes do you want? (1-9)").ToString();
                        int n = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(n);
                        results = GetRandomJokes(null, n);
                        PrintResults(results);
                    }
                }
                else if (key == 'q')
                {
                    break;
                }
                names = null;
            }


        }

        private static void PrintResults(string[] results)
        {
            printer.Value("[" + string.Join(",", results) + "]").ToString();
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

        private static String[] GetRandomJokes(string category, int number)
        {
            string[] jokes = new string[50];
            // new JsonFeed("https://api.chucknorris.io/", number);
            new JsonFeed("https://api.chucknorris.io/");
            for(int i=0; i<number; i++){
                jokes[i] = JsonFeed.GetRandomJoke(names?.Item1, names?.Item2, category);
            }
            for(int i=0; i<number; i++){
                Console.WriteLine(jokes[i]);
            }
            return jokes;
        }

        private static String[] getCategories()
        {
            new JsonFeed("https://api.chucknorris.io/jokes/categories");
            return JsonFeed.GetCategories();
        }

        private static Tuple<String,String> GetNames()
        {
            new JsonFeed("http://uinames.com/api/");
            dynamic result = JsonFeed.Getnames();
            Console.WriteLine(result);
            return Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
