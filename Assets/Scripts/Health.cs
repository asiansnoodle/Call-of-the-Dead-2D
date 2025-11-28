using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int max = 3;
    public UnityEvent onDeath;

    int current;
    public int Current => current;


    void Awake() => current = max;

    public void Take(int amount)
    {
        if (current <= 0) return;
        current -= amount;
        if (current <= 0) onDeath.Invoke();
    }
}
