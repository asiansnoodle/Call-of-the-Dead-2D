using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    public static DamageFlash I;

    public CanvasGroup canvasGroup;
    [Range(0f, 1f)]
    public float maxAlpha = 0.4f;   // how strong the flash is
    public float fadeSpeed = 5f;    // how fast it fades out

    void Awake()
    {
        I = this;

        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
    }

    void Update()
    {
        if (canvasGroup == null) return;

        // Fade back to transparent
        if (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha = Mathf.MoveTowards(
                canvasGroup.alpha,
                0f,
                fadeSpeed * Time.unscaledDeltaTime
            );
        }
    }

    public void Flash()
    {
        if (canvasGroup == null) return;

        // Instantly set to max, then Update() will fade it out
        canvasGroup.alpha = maxAlpha;
    }
}
