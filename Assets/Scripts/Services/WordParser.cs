using UnityEngine;

namespace FTRGames.Alpaseh.Core
{
    public class WordParser
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
                    'ı', 'I',
                    'i', 'İ',
                    's', 'S',
                    'ş', 'Ş',
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