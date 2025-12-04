using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("How long before the bullet auto-despawns (seconds)")]
    public float life = 3f;
    public int damage = 1;

    void Start() { Destroy(gameObject, life); }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Health>(out var hp))
        {
            hp.Take(damage);
        }
        Destroy(gameObject);
    }
}
