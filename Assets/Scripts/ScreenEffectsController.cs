using UnityEngine;
using UnityEngine.UI;

public class ScreenEffectsController : MonoBehaviour
{
    [SerializeField] private Image brightnessOverlay;

    private void Update()
    {
        if (brightnessOverlay != null)
        {
            Color c = brightnessOverlay.color;
            c.a = GameSettings.Brightness;  // 0–1 set with sliderdf 
            brightnessOverlay.color = c;
        }
    }
}
