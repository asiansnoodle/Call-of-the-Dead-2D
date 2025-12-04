using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker I;

    [Header("Shake Settings")]
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;
    public float dampingSpeed = 1.0f; // how fast shake ends

    private Vector3 initialPos;
    private float currentShakeTime;

    void Awake()
    {
        I = this;
        initialPos = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeTime > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
            shakeOffset.z = 0; // keep shake 

            transform.localPosition = initialPos + shakeOffset;

            currentShakeTime -= Time.deltaTime * dampingSpeed;

            if (currentShakeTime <= 0f)
            {
                transform.localPosition = initialPos; // reset
            }
        }
    }

    public void Shake(float duration = 0.1f, float magnitude = 0.1f)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        currentShakeTime = shakeDuration;
    }
}
