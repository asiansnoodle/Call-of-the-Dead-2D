using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I;

    [Header("Sources")]
    public AudioSource sfxSource;   

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

    void PlaySfx(AudioClip clip, float volumeScale = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.pitch = Random.Range(0.95f, 1.05f);
            sfxSource.PlayOneShot(clip, volumeScale);
        }
    }

    public void PlayShoot()       => PlaySfx(shootClip, 0.20f);
    public void PlayZombieDeath() => PlaySfx(zombieDeathClip);
    public void PlayWaveStart()   => PlaySfx(waveStartClip);
    public void PlayGameOver()    => PlaySfx(gameOverClip);
    public void PlayPlayerHurt()  => PlaySfx(playerHurtClip);
}
