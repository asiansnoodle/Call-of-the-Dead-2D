using UnityEngine;

public class FloatText : MonoBehaviour
{
    public float amplitude = 5f;   // how far it moves
    public float frequency = 1f;   // how fast it moves

    Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startPos + new Vector3(0, y, 0);
    }
}
