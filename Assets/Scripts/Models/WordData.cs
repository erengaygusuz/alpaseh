using System;
using System.Collections.Generic;
using System.Linq;

namespace FTRGames.Alpaseh.Models
{
    public class WordData
    {
        public List<List<string>> LevelWordList = new List<List<string>>();

        public WordData(string wordText, char[] identifiedLetters)
        {
            for (int i = 3; i < 8; i++)
            {
                AddWordsToList(wordText.Split('\n'), i, IdentifiedLetters(identifiedLetters));
            }
        }

        private List<Letter> IdentifiedLetters(char[] identifiedLetters)
        {
            var letters = new List<Letter>();

            for (int i = 0; i < identifiedLetters.Length; i = i + 2)
            {
                letters.Add(new Letter
                {
                    LowerCase = identifiedLetters[i],
                    UpperCase = identifiedLetters[i + 1]
                });
            }

            return letters;
        }

        private void AddWordsToList(string[] wordList, int wordSize, List<Letter> selectedLetters)
        {
            var tempList = new List<string>();

            for (int i = 0; i < wordList.Length; i++)
            {
                if (wordList[i].Length > 0)
                {
                    var selectedWord = wordList[i].Substring(0, wordList[i].Length - 1);

                    var selectedWordListCount = selectedWord.Length;

                    if (selectedWordListCount == wordSize)
                    {
                        var wordLetterCount = 0;

                        for (int k = 0; k < selectedWordListCount; k++)
                        {
                            for (int j = 0; j < selectedLetters.Count; j++)
                            {
                                selectedWord = selectedWord.ToLower();

                                var selectedWordLetter = selectedWord[k];
                                var selectedLetter = selectedLetters[j];

                                if (selectedWordLetter == selectedLetter.LowerCase)
                                {
                                    wordLetterCount++;
                                }
                            }
                        }

                        if (wordLetterCount == selectedWordListCount)
                        {
                            if (!tempList.Contains(selectedWord))
                            {
                                tempList.Add(selectedWord);
                            }
                        }
                    }
                }
            }

            LevelWordList.Add(ShuffleList(tempList));
        }

        private List<string> ShuffleList(List<string> list)
        {
            return list.OrderBy(a => Guid.NewGuid()).ToList();
        }
    }
}
