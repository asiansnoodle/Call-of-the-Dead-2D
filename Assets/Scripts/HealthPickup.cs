using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;  

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && col.TryGetComponent(out Health health))
        {
            health.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
