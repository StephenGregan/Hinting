using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Hinting
{
    public class Hinter
    {
        public static string ReadHintedLine<T, TResult>(IEnumerable<T> hintSource, 
            Func<T, TResult> hintField, string inputRegex = ".*", ConsoleColor hintColour = 
            ConsoleColor.DarkBlue)
        {
            ConsoleKeyInfo input;

            var suggestion = string.Empty;
            var userInput = string.Empty;
            var readLine = string.Empty;

            while (ConsoleKey.Enter != (input = Console.ReadKey()).Key)
            {
                if (input.Key == ConsoleKey.Backspace)
                
                    userInput = userInput.Any() ? userInput.Remove(userInput.Length - 1, 1) : string.Empty;
                
                else if (input.Key == ConsoleKey.Tab)
                
                    userInput = suggestion ?? userInput;
                
                else if (input != null && Regex.IsMatch(input.KeyChar.ToString(), inputRegex))
                
                    userInput += input.KeyChar;

                suggestion = hintSource.Select(item => hintField(item).ToString()).FirstOrDefault(
                    item => item.Length > userInput.Length && item.Substring(0, userInput.Length) == userInput);

                readLine = suggestion == null ? userInput : suggestion;

                ClearCurrentConsoleLine();

                Console.Write(userInput);

                var originalColour = Console.ForegroundColor;
                Console.ForegroundColor = hintColour;

                if (userInput.Any()) Console.Write(readLine.Substring(userInput.Length, readLine.Length - userInput.Length));

                Console.ForegroundColor = originalColour;
            }
            Console.WriteLine(readLine);
            return readLine;
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
