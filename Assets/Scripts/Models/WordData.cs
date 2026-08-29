using System;
using System.Collections.Generic;

namespace FTRGames.Alpaseh.Models
{
    public class WordData
    {
        private const int MinimumWordLength = 3;
        private const int MaximumWordLength = 7;

        private readonly Random random = new Random();

        public List<List<string>> LevelWordList { get; } = new List<List<string>>();

        public WordData(string wordText, char[] identifiedLetters)
        {
            if (wordText == null)
            {
                throw new ArgumentNullException(nameof(wordText));
            }

            HashSet<char> allowedLetters = BuildAllowedLetters(identifiedLetters);
            string[] words = ParseWords(wordText);

            for (int wordLength = MinimumWordLength; wordLength <= MaximumWordLength; wordLength++)
            {
                LevelWordList.Add(BuildWordList(words, wordLength, allowedLetters));
            }
        }

        private static HashSet<char> BuildAllowedLetters(char[] identifiedLetters)
        {
            if (identifiedLetters == null)
            {
                throw new ArgumentNullException(nameof(identifiedLetters));
            }

            if (identifiedLetters.Length % 2 != 0)
            {
                throw new ArgumentException(
                    "Identified letters must contain lower/upper case pairs.",
                    nameof(identifiedLetters));
            }

            var allowedLetters = new HashSet<char>();

            for (int i = 0; i < identifiedLetters.Length; i += 2)
            {
                allowedLetters.Add(identifiedLetters[i]);
            }

            return allowedLetters;
        }

        private static string[] ParseWords(string wordText)
        {
            return wordText.Split(
                new[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries);
        }

        private List<string> BuildWordList(
            string[] words,
            int wordLength,
            HashSet<char> allowedLetters)
        {
            var uniqueWords = new HashSet<string>(StringComparer.Ordinal);
            var result = new List<string>();

            foreach (string rawWord in words)
            {
                string word = rawWord.Trim().ToLowerInvariant();

                if (word.Length != wordLength || !UsesOnlyAllowedLetters(word, allowedLetters))
                {
                    continue;
                }

                if (uniqueWords.Add(word))
                {
                    result.Add(word);
                }
            }

            Shuffle(result);
            return result;
        }

        private static bool UsesOnlyAllowedLetters(string word, HashSet<char> allowedLetters)
        {
            foreach (char letter in word)
            {
                if (!allowedLetters.Contains(letter))
                {
                    return false;
                }
            }

            return true;
        }

        private void Shuffle(List<string> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int swapIndex = random.Next(i + 1);
                (list[i], list[swapIndex]) = (list[swapIndex], list[i]);
            }
        }
    }
}
