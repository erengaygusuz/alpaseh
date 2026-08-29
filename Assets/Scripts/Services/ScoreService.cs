using System.Collections.Generic;
using System.IO;
using System.Linq;
using FTRGames.Alpaseh.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public sealed class ScoreService
    {
        private const int MaxScoreCount = 100;
        private const string ScoreListFileName = "score-list.json";
        private const string EmptyScoreListJson = "[]";

        private readonly SortedSet<ScoreInfo> scoreList = new SortedSet<ScoreInfo>(new ScoreInfoScoreComparer());

        private string scoreListPath;

        public bool IsNewScoreAdded { get; set; }

        public void Initialization()
        {
            scoreListPath = Path.Combine(Application.persistentDataPath, ScoreListFileName);
            LoadScoreList();
        }

        public void UpdateScoreValues()
        {
            if (!IsNewScoreAdded)
            {
                return;
            }

            SaveScoreList();
            IsNewScoreAdded = false;
        }

        public bool CompareNewScoreWithScoresInTheList(int newScore)
        {
            if (newScore <= 0)
            {
                return false;
            }

            if (scoreList.Count < MaxScoreCount)
            {
                AddScore(newScore);
                return true;
            }

            ScoreInfo lowestScore = scoreList.Min;

            if (lowestScore == null || lowestScore.Score >= newScore)
            {
                return false;
            }

            scoreList.Remove(lowestScore);
            AddScore(newScore);
            return true;
        }

        public List<ScoreInfo> GetScoreList()
        {
            return scoreList.Reverse().ToList();
        }

        public void DeleteAllScoresFromTheList()
        {
            scoreList.Clear();
            SaveScoreList();
        }

        private void LoadScoreList()
        {
            EnsureScoreFileExists();
            scoreList.Clear();

            List<ScoreInfo> loadedScores = JsonConvert.DeserializeObject<List<ScoreInfo>>(
                File.ReadAllText(scoreListPath)) ?? new List<ScoreInfo>();

            for (int i = 0; i < loadedScores.Count; i++)
            {
                scoreList.Add(loadedScores[i]);
            }
        }

        private void EnsureScoreFileExists()
        {
            if (File.Exists(scoreListPath))
            {
                return;
            }

            File.WriteAllText(scoreListPath, EmptyScoreListJson);
        }

        private void SaveScoreList()
        {
            File.WriteAllText(scoreListPath, JsonConvert.SerializeObject(scoreList));
        }

        private void AddScore(int score)
        {
            scoreList.Add(new ScoreInfo
            {
                Username = PlayerPrefs.GetString(PlayerPrefsKeys.Username, string.Empty),
                Score = score
            });
        }
    }
}
