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

        public float Volume => audioView.loopAudioSource.volume;

        public float SavedVolume => PlayerPrefs.GetFloat(PlayerPrefsKeys.AudioLevel, 1.0f);

        public void Initialize()
        {
            var volume = SavedVolume;

            if (!PlayerPrefs.HasKey(PlayerPrefsKeys.AudioLevel))
            {
                PlayerPrefs.SetFloat(PlayerPrefsKeys.AudioLevel, volume);
                PlayerPrefs.Save();
            }

            SetVolume(volume);
        }

        public void SetVolume(float volume)
        {
            var clampedVolume = Mathf.Clamp01(volume);

            audioView.loopAudioSource.volume = clampedVolume;
            audioView.answerAudioSource.volume = clampedVolume;
            audioView.timeTickAudioSource.volume = clampedVolume;
            audioView.gameOverAudioSource.volume = clampedVolume;
            audioView.gameCompletedAudioSource.volume = clampedVolume;
        }

        public void SetVolumeAndSave(float volume)
        {
            SetVolume(volume);

            PlayerPrefs.SetFloat(PlayerPrefsKeys.AudioLevel, Volume);
            PlayerPrefs.Save();
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
