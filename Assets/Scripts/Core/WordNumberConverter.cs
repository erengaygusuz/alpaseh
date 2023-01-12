using System.Collections.Generic;

namespace FTRGames.Alpaseh.Core
{
    public class WordNumberConverter 
    {
        private static Dictionary<char, char> WordNumberPairs { get; set; }

        public static void Initialization()
        {
            CreateWordNumberPairs();
        }

        private static void CreateWordNumberPairs()
        {
            WordNumberPairs = new Dictionary<char, char>();

            WordNumberPairs.Add('o', '0');
            WordNumberPairs.Add('O', '0');
            WordNumberPairs.Add('e', '3');
            WordNumberPairs.Add('E', '3');
            WordNumberPairs.Add('z', '2');
            WordNumberPairs.Add('Z', '2');
            WordNumberPairs.Add('ý', '1');
            WordNumberPairs.Add('I', '1');
            WordNumberPairs.Add('i', '1');
            WordNumberPairs.Add('Ý', '1');
            WordNumberPairs.Add('s', '5');
            WordNumberPairs.Add('S', '5');
            WordNumberPairs.Add('þ', '5');
            WordNumberPairs.Add('Þ', '5');
            WordNumberPairs.Add('g', '6');
            WordNumberPairs.Add('G', '9');
            WordNumberPairs.Add('b', '8');
            WordNumberPairs.Add('B', '8');
            WordNumberPairs.Add('l', '7');
            WordNumberPairs.Add('L', '7');
            WordNumberPairs.Add('h', '4');
            WordNumberPairs.Add('H', '4');
        }

        public static string GetNumbersFromWord(string word)
        {
            string numberWord = "";

            for (int i = 0; i < word.Length; i++)
            {
                foreach (var wordNumber in WordNumberPairs)
                {
                    if (wordNumber.Key == word[i])
                    {
                        numberWord += wordNumber.Value;
                    }
                }
            }

            return GetWordReverseOrder(numberWord);
        }

        private static string GetWordReverseOrder(string word)
        {
            string tempWord = "";

            for (int i = word.Length - 1; i > -1; i--)
            {
                tempWord += word[i];
            }

            return tempWord;
        }
    }
}