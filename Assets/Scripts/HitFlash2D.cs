using System.Collections;
using UnityEngine;

public class HitFlash2D : MonoBehaviour
{
    [Header("Flash Settings")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.1f;

    [Header("Optional")]
    [Tooltip("If true, uses the original color of the sprite.")]
    [SerializeField] private bool useOriginalColor = true;

    private Color originalColor;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogWarning($"HitFlash2D on {gameObject.name} has no SpriteRenderer assigned.");
        }
    }

    public void Flash()
    {
        if (spriteRenderer == null) return;

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        Color beforeFlash = useOriginalColor ? originalColor : spriteRenderer.color;

        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = beforeFlash;
        flashCoroutine = null;
    }
}
