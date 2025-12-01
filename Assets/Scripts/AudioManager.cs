using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I;

    [Header("Sources")]
    public AudioSource sfxSource;   // for one-shot effects

    [Header("Clips")]
    public AudioClip shootClip;
    public AudioClip zombieDeathClip;
    public AudioClip waveStartClip;
    public AudioClip gameOverClip;

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

    void PlaySfx(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.pitch = Random.Range(0.95f, 1.05f);
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayShoot()      => PlaySfx(shootClip);
    public void PlayZombieDeath() => PlaySfx(zombieDeathClip);
    public void PlayWaveStart()  => PlaySfx(waveStartClip);
    public void PlayGameOver()   => PlaySfx(gameOverClip);
}
