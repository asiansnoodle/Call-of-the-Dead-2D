using System.Collections;
using UnityEngine;
using TMPro;

public class WaveAnnouncement : MonoBehaviour
{
    public static WaveAnnouncement I { get; private set; }

    [Header("References")]
    [SerializeField] private TextMeshProUGUI waveText;

    [Header("Timing (seconds)")]
    [SerializeField] private float fadeInDuration = 0.3f;
    [SerializeField] private float stayDuration = 1.0f;
    [SerializeField] private float fadeOutDuration = 0.5f;

    private CanvasGroup canvasGroup;
    private Coroutine currentRoutine;

    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;

        canvasGroup = GetComponent<CanvasGroup>();
        if (waveText == null)
        {
            waveText = GetComponent<TextMeshProUGUI>();
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;   // start hidden
        }
    }

    public void ShowWave(int waveNumber)
    {
        if (canvasGroup == null || waveText == null) return;

        // Stop any previous animation
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        waveText.text = $"Wave {waveNumber} Incoming!";
        currentRoutine = StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        // Fade in
        float t = 0f;
        while (t < fadeInDuration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / fadeInDuration);
            canvasGroup.alpha = normalized;
            yield return null;
        }

        canvasGroup.alpha = 1f;

        // Stay visible
        yield return new WaitForSeconds(stayDuration);

        // Fade out
        t = 0f;
        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / fadeOutDuration);
            canvasGroup.alpha = 1f - normalized;
            yield return null;
        }

        canvasGroup.alpha = 0f;
        currentRoutine = null;
    }
}
