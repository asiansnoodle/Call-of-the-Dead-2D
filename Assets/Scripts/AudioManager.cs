using UnityEngine;
using UnityEngine.Audio;   

public class AudioManager : MonoBehaviour
{
    public static AudioManager I;

    [Header("Mixer")]
    public AudioMixer mixer;   

    [Header("Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    [Header("Clips")]
    public AudioClip shootClip;
    public AudioClip zombieDeathClip;
    public AudioClip waveStartClip;
    public AudioClip gameOverClip;
    public AudioClip playerHurtClip;

    void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
    }

    // section for volume controls optionis

    public void SetMasterVolume(float value)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20f);
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
    }

    public void SetSfxVolume(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20f);
    }

    void PlaySfx(AudioClip clip, float volumeScale = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.pitch = Random.Range(0.95f, 1.05f);
            sfxSource.PlayOneShot(clip, volumeScale);
        }
    }

    public void PlayShoot()
    {
        PlaySfx(shootClip, 0.20f);
    }
    public void PlayZombieDeath()
    {
        PlaySfx(zombieDeathClip);
    }
    public void PlayWaveStart()
    {
        PlaySfx(waveStartClip);
    }
    public void PlayGameOver()
    {
        PlaySfx(gameOverClip);
    }
    public void PlayPlayerHurt()
    {
        PlaySfx(playerHurtClip);
    }
}
