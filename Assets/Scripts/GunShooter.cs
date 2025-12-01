using UnityEngine;

public class GunShooter : MonoBehaviour
{
    public Transform muzzle;            // assign Player->Muzzle
    public GameObject bulletPrefab;     // assign Bullet prefab
    public float bulletSpeed = 20f;

    // Optional: limit how fast the player can click-fire
    public float fireRate = 6f;         // max shots per second
    float nextShotAllowedAt = 0f;

    Collider2D playerCol;

    void Awake()
    {
        playerCol = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Semi-auto: fires only on the *click down* event
        if (Input.GetMouseButtonDown(0) && Time.time >= nextShotAllowedAt)
        {
            nextShotAllowedAt = Time.time + 1f / Mathf.Max(0.0001f, fireRate);
            Shoot();
        }
    }

    void Shoot()
    {
        var b = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
        var rb = b.GetComponent<Rigidbody2D>();
        rb.linearVelocity = muzzle.up * bulletSpeed;

        // Prevent immediate self-hit
        var bCol = b.GetComponent<Collider2D>();
        if (playerCol && bCol) Physics2D.IgnoreCollision(playerCol, bCol, true);

        // gunshot sound
        if (AudioManager.I != null)
        {
            AudioManager.I.PlayShoot();
        }
    }
}
