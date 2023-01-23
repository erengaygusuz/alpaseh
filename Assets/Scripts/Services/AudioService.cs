using FTRGames.Alpaseh.Views;
using UnityEngine;

namespace FTRGames.Alpaseh.Services
{
    public class AudioService
    {
        private readonly AudioView audioView;

        public AudioService(AudioView audioView)
        {
            this.audioView = audioView;
        }

        private void PlayAudio(AudioSource audioSource, AudioClip clip)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }

        public void StopAudio(AudioSource audioSource)
        {
            if (audioSource.clip != null)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
        }

        public void PlayMainMenuAudio()
        {
            PlayAudio(audioView.loopAudioSource, audioView.mainMenuAudio);
        }

        public void PlayWrongAnswerAudio()
        {
            PlayAudio(audioView.answerAudioSource, audioView.wrongAnswerAudio);
        }

        public void PlayCorrectAnswerAudio()
        {
            PlayAudio(audioView.answerAudioSource, audioView.correctAnswerAudio);
        }

        public void PlayTimeTickAudio()
        {
            PlayAudio(audioView.timeTickAudioSource, audioView.timeTickAudio);
        }

        public void PlayGameOverAudio()
        {
            PlayAudio(audioView.gameOverAudioSource, audioView.gameOverAudio);
        }

        public void PlayGameCompletedAudio()
        {
            PlayAudio(audioView.gameCompletedAudioSource, audioView.gameCompletedAudio);
        }

        public void PlayGameSceneAudio()
        {
            PlayAudio(audioView.loopAudioSource, audioView.gameSceneAudio);
        }

        public void StopTimeTickAudio()
        {
            StopAudio(audioView.timeTickAudioSource);
        }
    }
}