using FTRGames.Alpaseh.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public class ScoreService
    {
        private SortedSet<ScoreInfo> ScoreList { get; set; }

        public bool IsNewScoreAdded { get; set; }

        private string scoreListPath;

        public void Initialization()
        {
            ScoreListPathInit();

            ScoreListInit();

            GetScoreListFromJSON();
        }

        public void UpdateScoreValues()
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

        private void SetScoreListToJSON()
        {
            File.WriteAllText(scoreListPath, JsonConvert.SerializeObject(ScoreList));
        }

        public bool CompareNewScoreWithScoresInTheList(int newScore)
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

        public List<ScoreInfo> GetScoreList()
        {
            return ScoreList.Reverse().ToList();
        }

        public void DeleteAllScoresFromTheList()
        {
            ScoreList.Clear();
            SetScoreListToJSON();
        }
    }
}
