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

        public WordData(string wordText, string allowedLetters)
        {
            if (wordText == null)
            {
                throw new ArgumentNullException(nameof(wordText));
            }

            HashSet<char> allowedLetterSet = BuildAllowedLetters(allowedLetters);
            string[] words = ParseWords(wordText);

            for (int wordLength = MinimumWordLength; wordLength <= MaximumWordLength; wordLength++)
            {
                LevelWordList.Add(BuildWordList(words, wordLength, allowedLetterSet));
            }
        }

        private static HashSet<char> BuildAllowedLetters(string allowedLetters)
        {
            if (string.IsNullOrWhiteSpace(allowedLetters))
            {
                throw new ArgumentException(
                    "Allowed letters cannot be null or empty.",
                    nameof(allowedLetters));
            }

            var allowedLetterSet = new HashSet<char>();

            foreach (char letter in allowedLetters.Trim().ToLowerInvariant())
            {
                if (!char.IsWhiteSpace(letter))
                {
                    allowedLetterSet.Add(letter);
                }
            }

            if (allowedLetterSet.Count == 0)
            {
                throw new ArgumentException(
                    "Allowed letters must contain at least one character.",
                    nameof(allowedLetters));
            }

            return allowedLetterSet;
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
