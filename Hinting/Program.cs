using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinting
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var countries = new List<string>()
            {
                "Brazil",
                "Ireland",
                "England",
                "Spain",
                "Mexico"
            };

            Console.WriteLine("Available countries:");
            Console.WriteLine();

            foreach (var country in countries)
            {
                Console.WriteLine(country);
            }

            Console.WriteLine();
            Console.WriteLine("Please enter a country:");

            var input = Hinter.ReadHintedLine(countries, country => country);

            Console.WriteLine();
            Console.WriteLine($"You entered: {input}");
            Console.ReadLine();
        }
    }
}
