using UnityEngine;

public class PlayerBounds2D : MonoBehaviour
{
    [Header("Bounds")]
    [SerializeField] private bool useBounds = true;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    [Header("Offset (Optional)")]
    [Tooltip("Use this if your player pivot is not exactly at their feet/center.")]
    [SerializeField] private Vector2 pivotOffset = Vector2.zero;

    private void LateUpdate()
    {
        if (!useBounds) return;

        Vector3 pos = transform.position;

        float x = pos.x + pivotOffset.x;
        float y = pos.y + pivotOffset.y;

        x = Mathf.Clamp(x, minBounds.x, maxBounds.x);
        y = Mathf.Clamp(y, minBounds.y, maxBounds.y);

        pos.x = x - pivotOffset.x;
        pos.y = y - pivotOffset.y;

        transform.position = pos;
    }

// used for showing the green line on editor where the bounds are
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!useBounds) return;

        Gizmos.color = Color.green;

        Vector3 center = new Vector3(
            (minBounds.x + maxBounds.x) * 0.5f,
            (minBounds.y + maxBounds.y) * 0.5f,
            0f
        );

        Vector3 size = new Vector3(
            Mathf.Abs(maxBounds.x - minBounds.x),
            Mathf.Abs(maxBounds.y - minBounds.y),
            0f
        );

        Gizmos.DrawWireCube(center, size);
    }
#endif
}
