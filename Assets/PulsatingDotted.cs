
using UnityEngine;

public class PulsatingDotted : MonoBehaviour
{
    private Vector3 cachePos;
    private Vector3 scaleSiz = Vector3.one * 2f;

    void OnEnable()
    {
        cachePos = transform.localScale;
        LeanTween.scale(gameObject, scaleSiz, 0.3f).setEase(LeanTweenType.punch).setLoopClamp();
    }

    void OnDisable()
    {
        LeanTween.cancel(gameObject);
        transform.localScale = cachePos;
    }
}
