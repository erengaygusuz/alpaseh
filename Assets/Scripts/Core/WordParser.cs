using UnityEngine;

namespace FTRGames.Alpaseh.Core
{
    public class WordParser
    {
        [SerializeField]
        private TextAsset[] wordText;
        private static char[] IdentifiedLetters { get; set; }
        public static WordData WordDatas { get; set; }

        public static void Initialization()
        {
            LetterIdentification();
            ProcessData();
        }

        private static void LetterIdentification()
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

        private static void ProcessData()
        {
            WordDatas = new WordData(wordText[PlayerPrefs.GetInt("Alpaseh-SelectedLanguageIndex", 0)].text, IdentifiedLetters);
        }
    }
}