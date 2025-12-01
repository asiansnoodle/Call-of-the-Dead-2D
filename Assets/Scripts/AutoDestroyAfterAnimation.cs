using System.Collections;
using UnityEngine;

public class AutoDestroyAfterAnimation : MonoBehaviour
{
    [Header("Timing")]
    [Tooltip("If left at 0, uses the current animation state's length.")]
    [SerializeField] private float overrideLifetime = 0f;

    [Header("Stain Spawn")]
    [SerializeField] private GameObject stainPrefab;
    [SerializeField] private Vector3 stainOffset = Vector3.zero;
    [SerializeField] private bool inheritRotation = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        float lifeTime = overrideLifetime;

        if (lifeTime <= 0f && animator != null)
        {
            lifeTime = animator.GetCurrentAnimatorStateInfo(0).length;
        }

        StartCoroutine(HandleLifeCycle(lifeTime));
    }

    private IEnumerator HandleLifeCycle(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        if (stainPrefab != null)
        {
            Quaternion rot = inheritRotation ? transform.rotation : Quaternion.identity;

            Instantiate(
                stainPrefab,
                transform.position + stainOffset,
                rot
            );
        }

        Destroy(gameObject);
    }
}
