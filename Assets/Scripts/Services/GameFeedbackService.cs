using FTRGames.Alpaseh.Views;
using UnityEngine;
using UnityEngine.Events;

namespace FTRGames.Alpaseh.Services
{
    public sealed class GameFeedbackService
    {
        private readonly TweenService tweenService;

        public UnityEvent CorrectAnswerCompleted => tweenService.playCorrectAnswerAnimEvent;
        public UnityEvent WrongAnswerCompleted => tweenService.playWrongAnswerAnimEvent;
        public UnityEvent FeedbackCompleted => tweenService.tweenTextEvent;

        public GameFeedbackService(TweenService tweenService)
        {
            this.tweenService = tweenService;
        }

        public void Initialization()
        {
            tweenService.Initialization();
        }

        public void PlayAnswerFeedback(GameView gameView, bool isCorrectAnswer)
        {
            if (isCorrectAnswer)
            {
                tweenService.PlayCorrectAnswerAnim(gameView.enteredNumberWordText, gameView.checkButton);
                return;
            }

            tweenService.PlayWrongAnswerAnim(gameView.enteredNumberWordText, gameView.checkButton);
        }

        public void ShowEarnScore(GameView gameView, LevelService levelService)
        {
            gameView.scoreIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.scoreIncDecObj,
                levelService.GetActiveLevel().EarnedScoreAmount.ToString(),
                Color.green,
                true,
                gameView.checkButton);
        }

        public void ShowEarnTime(GameView gameView, LevelService levelService)
        {
            gameView.timeIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.timeIncDecObj,
                levelService.GetActiveLevel().EarnedTimeAmount.ToString(),
                Color.green,
                true,
                gameView.checkButton);
        }

        public void ShowEarnLife(GameView gameView, LevelService levelService)
        {
            gameView.lifeIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.lifeIncDecObj,
                levelService.GetActiveLevel().LifeIncreaseAmount.ToString(),
                Color.green,
                true,
                gameView.checkButton);
        }

        public void ShowLoseTime(GameView gameView, LevelService levelService)
        {
            gameView.timeIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.timeIncDecObj,
                levelService.GetActiveLevel().LoseTimeAmount.ToString(),
                Color.red,
                false,
                gameView.checkButton);
        }

        public void ShowLoseLife(GameView gameView, LevelService levelService)
        {
            gameView.lifeIncDecObj.SetActive(true);
            tweenService.TweenText(
                gameView.lifeIncDecObj,
                levelService.GetActiveLevel().LoseLifeAmount.ToString(),
                Color.red,
                false,
                gameView.checkButton);
        }
    }
}
