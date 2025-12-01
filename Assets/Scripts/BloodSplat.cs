using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    [Header("Blood Variants")]
    [SerializeField] private Sprite[] bloodSprites;

    [Header("Randomization")]
    [SerializeField] private bool randomRotation = true;
    [SerializeField] private float minScale = 2.0f;
    [SerializeField] private float maxScale = 2.8f;

    [Header("Lifetime (Optional)")]
    [SerializeField] private bool fadeOut = false;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float fadeDuration = 1.5f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Choose a random sprite
        if (bloodSprites != null && bloodSprites.Length > 0)
        {
            spriteRenderer.sprite = bloodSprites[Random.Range(0, bloodSprites.Length)];
        }

        // Random rotation
        if (randomRotation)
        {
            float angle = Random.Range(0f, 360f);
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        // Random scale
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, 1f);

        // Optional fade-out
        if (fadeOut)
        {
            StartCoroutine(FadeRoutine());
        }
    }

    private System.Collections.IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);

        float t = 0f;
        Color startColor = spriteRenderer.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float normalized = Mathf.Clamp01(t / fadeDuration);
            spriteRenderer.color = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0f), normalized);
            yield return null;
        }

        Destroy(gameObject);
    }
}
