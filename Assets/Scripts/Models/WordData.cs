using System.Collections.Generic;

namespace FTRGames.Alpaseh.Models
{
    public class WordData
    {
        public List<List<string>> LevelWordList = new List<List<string>>();

        public WordData(string wordText, char[] identifiedLetters)
        {
            for (int i = 3; i < 9; i++)
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
            var count = 0;

            var tempList = new List<string>();

            for (int i = 0; i < wordList.Length; i++)
            {
                var isSuitableWord = false;

                var selectedWord = wordList[i].Substring(0, wordList[i].Length - 1);

                var selectedWordListCount = selectedWord.Length;

                if (selectedWordListCount == wordSize)
                {
                    var wordLetterCount = 0;

                    for (int k = 0; k < selectedWordListCount; k++)
                    {
                        for (int j = 0; j < selectedLetters.Count; j++)
                        {
                            var selectedWordLetter = wordList[i][k];
                            var selectedLetter = selectedLetters[j];

                            if (selectedWordLetter == selectedLetter.UpperCase || selectedWordLetter == selectedLetter.LowerCase)
                            {
                                wordLetterCount++;
                            }
                        }
                    }

                    if (wordLetterCount == selectedWordListCount)
                    {
                        if (tempList.Count > 0)
                        {
                            var lastAddedItem = tempList[tempList.Count - 1];

                            if ((lastAddedItem != wordList[i]) && i != 0)
                            {
                                isSuitableWord = true;
                            }
                        }

                        else
                        {
                            isSuitableWord = true;
                        }
                    }

                    if (isSuitableWord == true)
                    {
                        count++;

                        tempList.Add(wordList[i]);
                    }
                }
            }

            LevelWordList.Add(tempList);
        }
    }
}
