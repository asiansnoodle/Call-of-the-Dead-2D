using UnityEngine;

public class PlayerAim2D : MonoBehaviour
{
    public Camera cam; // assign main camera in Inspector
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!cam) cam = Camera.main;
    }

    void Update()
    {
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouseWorld = cam.ScreenToWorldPoint(mouseScreen);
        Vector2 lookDir = (mouseWorld - transform.position);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
