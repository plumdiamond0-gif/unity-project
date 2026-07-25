using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioTable audioTable;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Default Clips")]
    public AudioClip buttonClickSound;

    public void Init()
    {
        GM.GetAssetManager().LoadAsset<AudioTable>("AudioTable", 
        (go)=>
        {
            audioTable = go;
            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
            bgmSource.volume = 0.2f;
            Debug.Log("AudioTable 불러오기");
        });
    }
    public void PlayBGM(AudioType audioType, bool loop = true)
    {
        AudioClip clip = audioTable.audioDatas.Find(x => x.audioType == audioType).audioClip; 
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
    public void PlaySFX(AudioType audioType)
    {
        AudioClip clip = audioTable.audioDatas.Find(x => x.audioType == audioType).audioClip;
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}