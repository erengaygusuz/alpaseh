using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Alpaseh/Data/Game Config")]
    public sealed class GameConfig : ScriptableObject
    {
        [SerializeField, Min(0.0f)]
        private float initialTime = 200.0f;

        [SerializeField, Min(0.0f)]
        private float initialLife = 10.0f;

        public float InitialTime => initialTime;
        public float InitialLife => initialLife;
    }
}
