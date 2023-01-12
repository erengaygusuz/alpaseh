using FTRGames.Alpaseh.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FTRGames.Alpaseh.Core
{
    public class ScoreManager : MonoBehaviour
    {
        private static SortedSet<ScoreInfo> ScoreList { get; set; }

        public static bool IsNewScoreAdded { get; set; }

        private static string scoreListPath;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            ScoreListPathInit();

            ScoreListInit();

            GetScoreListFromJSON();
        }

        private void Update()
        {
            if (IsNewScoreAdded)
            {
                SetScoreListToJSON();

                IsNewScoreAdded = false;
            }
        }

        private void ScoreListPathInit()
        {
            scoreListPath = Application.persistentDataPath + "/" + "score-list.json";
        }

        private void ScoreListInit()
        {
            ScoreList = new SortedSet<ScoreInfo>(new ScoreInfoScoreComparer());
        }

        private void GetScoreListFromJSON()
        {
            if (!File.Exists(scoreListPath))
            {
                FileStream fileStream = File.Create(scoreListPath);

                Byte[] emptyContent = new UTF8Encoding(true).GetBytes("[]");
                fileStream.Write(emptyContent, 0, emptyContent.Length);
                fileStream.Close();
            }

            List<ScoreInfo> tempScoreList = JsonConvert.DeserializeObject<List<ScoreInfo>>(File.ReadAllText(scoreListPath));

            for (int i = 0; i < tempScoreList.Count; i++)
            {
                ScoreList.Add(tempScoreList[i]);
            }
        }

        private static void SetScoreListToJSON()
        {
            File.WriteAllText(scoreListPath, JsonConvert.SerializeObject(ScoreList));
        }

        public static bool CompareNewScoreWithScoresInTheList(int newScore)
        {
            if (newScore > 0)
            {
                if (ScoreList.Count < 100)
                {
                    ScoreList.Add(new ScoreInfo { Username = PlayerPrefs.GetString("Alpaseh-Username", ""), Score = newScore });

                    return true;
                }

                else
                {
                    if (ScoreList.Min.Score < newScore)
                    {
                        ScoreList.Remove(ScoreList.Min);
                        ScoreList.Add(new ScoreInfo { Username = PlayerPrefs.GetString("Alpaseh-Username", ""), Score = newScore });

                        return true;
                    }
                }
            }

            return false;
        }

        public static List<ScoreInfo> GetScoreList()
        {
            return ScoreList.Reverse().ToList();
        }

        public static void DeleteAllScoresFromTheList()
        {
            ScoreList.Clear();
            SetScoreListToJSON();
        }
    }
}
