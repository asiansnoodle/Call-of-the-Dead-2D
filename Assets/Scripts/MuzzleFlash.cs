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

        // Remember the size/rotation you set in the editor
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
        // random size & angle *relative* to your edited transform
        float s = Random.Range(scaleRange.x, scaleRange.y);
        transform.localScale = baseScale * s;
        transform.localRotation = baseRotation * Quaternion.Euler(0f, 0f, Random.Range(-angleJitter, angleJitter));

        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.enabled = false;

        // reset to base in case something changed mid-game
        transform.localScale = baseScale;
        transform.localRotation = baseRotation;
    }
}
