using System.Collections.Generic;
using UnityEngine;

public class WordParser : Singleton<WordParser>
{
    [SerializeField]
    private TextAsset[] wordText;
    private char[] IdentifiedLetters { get; set; }
    public WordData WordDatas { get; set; }

    public void Initialization()
    {
        LetterIdentification();
        ProcessData();
    }

    private void LetterIdentification()
    {
        if (PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex", 0) == 0)
        {
            IdentifiedLetters = new char[]
            {
                    'o', 'O',
                    'e', 'E',
                    'z', 'Z',
                    'i', 'I',
                    's', 'S',
                    'g', 'G',
                    'b', 'B',
                    'l', 'L',
                    'h', 'H'
            };
        }

        else
        {
            IdentifiedLetters = new char[]
            {
                    'o', 'O',
                    'e', 'E',
                    'z', 'Z',
                    'ý', 'I',
                    'i', 'Ý',
                    's', 'S',
                    'þ', 'Þ',
                    'g', 'G',
                    'b', 'B',
                    'l', 'L',
                    'h', 'H'
            };
        }
    }

    private void ProcessData()
    {
        WordDatas = new WordData(wordText[PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex", 0)].text, IdentifiedLetters);
    }
}

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
        List<Letter> letters = new List<Letter>();

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
        int count = 0;

        List<string> tempList = new List<string>();

        for (int i = 0; i < wordList.Length; i++)
        {
            bool isSuitableWord = false;

            if (wordList[i].Length == wordSize)
            {
                int wordLetterCount = 0;

                for (int k = 0; k < wordList[i].Length; k++)
                {
                    for (int j = 0; j < selectedLetters.Count; j++)
                    {
                        if (wordList[i][k] == selectedLetters[j].UpperCase || wordList[i][k] == selectedLetters[j].LowerCase)
                        {
                            wordLetterCount++;
                        }
                    }
                }

                if (wordLetterCount == wordList[i].Length)
                {
                    if (tempList.Count > 0)
                    {
                        if ((tempList[tempList.Count - 1] != wordList[i]) && i != 0)
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

public class Letter
{
    public char UpperCase { get; set; }
    public char LowerCase { get; set; }
}