using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ZombieAI : MonoBehaviour
{
    public float speed = 2.8f;
    public int contactDamage = 1;
    public float hitCooldown = 0.6f;

    Rigidbody2D rb;
    Transform target;
    float cd;

    void Awake() { rb = GetComponent<Rigidbody2D>(); }
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player) target = player.transform;
    }

    void FixedUpdate()
    {
        if (!target) return;
        Vector2 toPlayer = ((Vector2)target.position - rb.position).normalized;
        rb.MovePosition(rb.position + toPlayer * speed * Time.fixedDeltaTime);

        // face movement direction (optional)
        float angle = Mathf.Atan2(toPlayer.y, toPlayer.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        if (cd > 0) cd -= Time.fixedDeltaTime;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (cd > 0) return;
        if (col.gameObject.CompareTag("Player") && col.gameObject.TryGetComponent<Health>(out var hp))
        {
            hp.Take(contactDamage);
            cd = hitCooldown;
        }
    }
}
