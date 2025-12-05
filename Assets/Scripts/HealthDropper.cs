using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthDropper : MonoBehaviour
{
    [Header("Drop Settings")]
    [Range(0f, 1f)]
    public float dropChance = 0.2f;  

    [Header("Pickup Prefab")]
    public GameObject healthPickupPrefab; 

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();

        if (health != null)
        {
            health.onDeath.AddListener(OnDied);
        }
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.onDeath.RemoveListener(OnDied);
        }
    }

    private void OnDied()
    {
        if (healthPickupPrefab == null) return;

        if (Random.value < dropChance)
        {
            Instantiate(
                healthPickupPrefab,
                transform.position,
                Quaternion.identity
            );
        }
    }
}
