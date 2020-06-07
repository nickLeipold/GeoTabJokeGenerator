using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChuckNorrisJokeGenerator
{
    public class ConsolePrinter
    {
        bool _print;
        public ConsolePrinter(bool print)
        {
            this._print = print;
        }
        public static object PrintValue;

        public ConsolePrinter Value(string value)
        {
            PrintValue = value;
            return this;
        }

        public override string ToString()
        {
            if(_print)
                Console.WriteLine(PrintValue);
            return null;
        }
        //forces the print even if not in print mode
        public string ToStringForce()
        {
            Console.WriteLine(PrintValue);
            return null;
        }
    }
}
