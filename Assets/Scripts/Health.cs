using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public int max = 3;
    public UnityEvent onDeath;

    int current;
    public int Current => current;
    private HitFlash2D hitFlash;

    [Header("Death Effect")]
    [SerializeField] private GameObject deathEffectPrefab;

    void Awake()
    {
        current = max;
        hitFlash = GetComponent<HitFlash2D>();
    }

    public void Take(int amount)
    {
        if (current <= 0) return;

        current -= amount;

        if (hitFlash != null && current > 0)
        {
            hitFlash.Flash();
        }

        if (CompareTag("Player") && current != 0)
        {
            if (AudioManager.I != null)
                AudioManager.I.PlayPlayerHurt();

            if (DamageFlash.I != null)
                DamageFlash.I.Flash();

            if (CameraShaker.I != null)
                CameraShaker.I.Shake(0.3f, 0.3f);   
        }

        if (current <= 0)
        {
            current = 0;
            onDeath?.Invoke();

            if (deathEffectPrefab != null)
            {
                Instantiate(
                    deathEffectPrefab,
                    transform.position,
                    Quaternion.identity
                );
            }

            Destroy(gameObject);
        }
    }


    public void Heal(int amount)
    {
        if (current <= 0) return; 
        current = Mathf.Min(current + amount, max);
    }

    public void SetMax(int newMax, bool healToFull = true)
    {
        max = newMax;

        if (healToFull)
        {
            current = max;
        }
        else
        {
            current = Mathf.Min(current, max);
        }
    }

    public void HealToFull()
    {
        current = max;
    }
}
