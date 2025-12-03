using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialOverlay : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup tutorialCanvasGroup;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1.2f;

    private bool hasShotYet = false;
    private bool isFading = false;

    private void Start()
    {
        // Ensure fully visible on start
        if (tutorialCanvasGroup != null)
            tutorialCanvasGroup.alpha = 1f;

        // Subscribe to your gun shoot event (we’ll add tiny line there)
        GunShooter.OnPlayerShoot += HandlePlayerShoot;
    }

    private void OnDestroy()
    {
        GunShooter.OnPlayerShoot -= HandlePlayerShoot;
    }

    private void HandlePlayerShoot()
    {
        if (hasShotYet || isFading) return;

        hasShotYet = true;
        StartCoroutine(FadeOutRoutine());
    }

    private System.Collections.IEnumerator FadeOutRoutine()
    {
        isFading = true;

        float elapsed = 0f;
        float startAlpha = tutorialCanvasGroup.alpha;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; // unaffected by timescale
            float t = Mathf.Clamp01(elapsed / fadeDuration);

            tutorialCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t);

            yield return null;
        }

        tutorialCanvasGroup.alpha = 0f;

        // fully disable
        tutorialCanvasGroup.gameObject.SetActive(false);
    }
}
