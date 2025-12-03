using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor Settings")]
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 hotspot = Vector2.zero;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;

    private void Start()
    {
        if (cursorTexture == null)
        {
            Debug.LogWarning("CursorManager: No cursorTexture assigned!");
            return;
        }

        hotspot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);

        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }

    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}
