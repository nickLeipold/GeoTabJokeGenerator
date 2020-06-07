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

        static ConsolePrinter printer = new ConsolePrinter();

        static void Main(string[] args)
        {
            Boolean instructionsOn;
            Tuple<string, string> names = null;
            //checks to see if we are in api mode which doesnt need to output menus
            Console.WriteLine(args.Length + " : number of args");
            if (args.Length == 1 && string.Equals(args[0], "--api"))
            {
                instructionsOn = false;
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
                    PrintResults(results);
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
                    if(decision){
                        category = selectCategory();
                        Console.WriteLine("debug: selected category:  " + category);
                    }

                    printer.Value("How many jokes do you want? (1-9)").ToString();
                    int n = Int32.Parse(Console.ReadLine());
                    results = GetRandomJokes(category, n, names);
                    PrintResults(results);
                    
                }
                else if (key == 'q')
                {
                    break;
                }
            }


        }


        private static string selectCategory(){
            string category;
            Console.WriteLine("Here are the available categories:");
            results = getCategories();
            Console.WriteLine("Here are the available categories:");
            PrintResults(results, 4);
            category = Console.ReadLine();
            if(results.Contains(category.ToLower())){
                return category.ToLower();
            }else {
                //will recursively call itself if the input category is not available.
                Console.WriteLine("The category '" + category + "' is not a valid option, please choose again");
                return selectCategory();
            }
        }
        private static bool detectBooleanResponse(char key)
        {
            if(key == 'y'){
                return true;
            }
            return false;
        }

        private static void PrintResults(string[] results, int indent = 0)
        {
            string spaces= "";
            for(int i =0; i < indent; i++){
                spaces += " ";
            }
            for(int i=0; i < results.Length; i++){
                printer.Value(spaces + results[i]).ToString();
            }
            // printer.Value(string.Join("\n", results)).ToString();
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

        private static String[] GetRandomJokes(string category, int number, Tuple<string, string> names = null)
        {
            if(names == null){
                names = new Tuple<string,string>(null,null);
            }
            string[] jokes = new string[number]; //hard coded max, should be made dynamic if time allows
            new JsonFeed("https://api.chucknorris.io/");
            //Console.WriteLine(names.Item1 + " " + names?.Item1);
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
                    int index = jokes[i].IndexOf("Chuck Norris");
                    string firstPart = jokes[i].Substring(0, index);
                    string secondPart = jokes[i].Substring(0 + index + "Chuck Norris".Length, jokes[i].Length - (index + "Chuck Norris".Length));
                    jokes[i] = firstPart + " " + firstname + " " + lastname + secondPart;
                }
                Console.WriteLine(jokes[i]);
            }
            return jokes;
        }

        private static String[] getCategories()
        {
            new JsonFeed("https://api.chucknorris.io/");
            return JsonFeed.GetCategories();
        }

        private static Tuple<String, String> GetNames()
        {
            new JsonFeed("https://names.privserv.com/");
            dynamic result = JsonFeed.Getnames();
            Console.WriteLine(result);
            return Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
    }
}
