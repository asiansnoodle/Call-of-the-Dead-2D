// using UnityEngine;
// using UnityEngine.Events;

// public class Health : MonoBehaviour
// {
//     public int max = 3;
//     public UnityEvent onDeath;

//     int current;
//     public int Current => current;


//     void Awake() => current = max;

//     public void Take(int amount)
//     {
//         if (current <= 0) return;
//         current -= amount;
//         if (current <= 0) onDeath.Invoke();
//     }

//     public void HealToFull()
//     {
//         current = max;
//     }

// }

using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health")]
    public int max = 3;
    public UnityEvent onDeath;

    int current;
    public int Current => current;

    void Awake()
    {
        current = max;
    }

    public void Take(int amount)
    {
        if (current <= 0) return;

        current -= amount;

        if (CompareTag("Player") && current != 0)
        {
            if (AudioManager.I != null)
                AudioManager.I.PlayPlayerHurt();
        }

        if (current <= 0)
        {
            current = 0;
            onDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    // Optional helpers you can use from WaveManager, powerups, etc.

    public void Heal(int amount)
    {
        if (current <= 0) return; // dead things don't heal
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
