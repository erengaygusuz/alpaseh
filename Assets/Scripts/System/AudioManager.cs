using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioClip mainMenuAudio;
    [SerializeField]
    private AudioClip gameSceneAudio;
    public AudioSource loopAudioSource;

    [SerializeField]
    private AudioClip wrongAnswerAudio;
    [SerializeField]
    private AudioClip correctAnswerAudio;
    public AudioSource answerAudioSource;

    [SerializeField]
    private AudioClip timeTickAudio;
    public AudioSource timeTickAudioSource;

    [SerializeField]
    private AudioClip gameOverAudio;
    public AudioSource gameOverAudioSource;

    private void Start()
    {
        DontDestroyOnLoad(this);
        AudioLevelKeyInit();
    }

    private void AudioLevelKeyInit()
    {
        if (!PlayerPrefs.HasKey("Alpaseh-AudioLevelSliderValue"))
        {
            PlayerPrefs.SetFloat("Alpaseh-AudioLevelSliderValue", 1.0f);
            PlayerPrefs.Save();
        }

        else
        {
            loopAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
            answerAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
            timeTickAudioSource.volume = PlayerPrefs.GetFloat("Alpaseh-AudioLevelSliderValue");
        }
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
        PlayAudio(loopAudioSource, mainMenuAudio);
    }

    public void PlayWrongAnswerAudio()
    {
        PlayAudio(answerAudioSource, wrongAnswerAudio);
    }

    public void PlayCorrectAnswerAudio()
    {
        PlayAudio(answerAudioSource, correctAnswerAudio);
    }

    public void PlayTimeTickAudio()
    {
        PlayAudio(timeTickAudioSource, timeTickAudio);
    }

    public void PlayGameOverAudio()
    {
        PlayAudio(gameOverAudioSource, gameOverAudio);
    }

    public void PlayGameSceneAudio()
    {
        PlayAudio(loopAudioSource, gameSceneAudio);
    }
}