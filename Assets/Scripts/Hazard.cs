using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Header("Damage Settings")]
    public int damage = 1;        // tick damage
    public float tickRate = 0.5f; 

    private float timer;

    private void OnTriggerStay2D(Collider2D col)
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (col.TryGetComponent<Health>(out Health h))
            {
                h.Take(damage);
            }

            timer = tickRate;
        }
    }
}
