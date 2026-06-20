using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Default Clips")]
    public AudioClip buttonClickSound;


    private void Awake()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();
    }
    // ======================
    // BGM
    // ======================

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PauseBGM()
    {
        bgmSource.Pause();
    }

    public void ResumeBGM()
    {
        bgmSource.UnPause();
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    // ======================
    // SFX
    // ======================

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    // ======================
    // UI
    // ======================

    public void PlayButtonClick()
    {
        if (buttonClickSound != null)
            sfxSource.PlayOneShot(buttonClickSound);
    }
}