using System.Collections;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public float flashTime = 0.05f;
    public Vector2 scaleRange = new Vector2(0.8f, 1.2f);
    public float angleJitter = 10f;

    private Vector3 baseScale;
    private Quaternion baseRotation;

    void Awake()
    {
        if (!spriteRenderer)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        baseScale = transform.localScale;
        baseRotation = transform.localRotation;
    }

    public void Flash()
    {
        if (spriteRenderer == null) return;

        StopAllCoroutines();
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        float s = Random.Range(scaleRange.x, scaleRange.y);
        transform.localScale = baseScale * s;
        transform.localRotation = baseRotation * Quaternion.Euler(0f, 0f, Random.Range(-angleJitter, angleJitter));

        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.enabled = false;

        transform.localScale = baseScale;
        transform.localRotation = baseRotation;
    }
}
