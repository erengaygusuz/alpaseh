using FTRGames.Alpaseh.Models;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public class WordParserService
    {
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
                    'ı', 'I',
                    's', 'S',
                    'g', 'G',
                    'b', 'B',
                    'l', 'L',
                    'h', 'H'
                };
            }
        }

        private void ProcessData()
        {
            wordText = Resources.LoadAll<TextAsset>("WordList/");
            WordDatas = new WordData(wordText[PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex", 0)].text, IdentifiedLetters);
        }
    }
}