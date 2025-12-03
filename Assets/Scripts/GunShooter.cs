using UnityEngine;

public class GunShooter : MonoBehaviour
{
    public static System.Action OnPlayerShoot;

    public Transform muzzle;            
    public GameObject bulletPrefab;     
    public float bulletSpeed = 20f;
    public MuzzleFlash muzzleFlash;

    public float fireRate = 6f;         
    float nextShotAllowedAt = 0f;

    Collider2D playerCol;

    void Awake()
    {
        playerCol = GetComponent<Collider2D>();
    }

    void Update()
    {
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

        // for no self-hits
        var bCol = b.GetComponent<Collider2D>();
        if (playerCol && bCol) Physics2D.IgnoreCollision(playerCol, bCol, true);

        // gunshot sound
        if (AudioManager.I != null)
        {
            AudioManager.I.PlayShoot();
        }
        
        if (muzzleFlash != null){
            muzzleFlash.Flash();
        }

        OnPlayerShoot?.Invoke();
    }
}
