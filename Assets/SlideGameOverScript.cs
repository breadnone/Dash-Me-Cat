using UnityEngine;

public class SlideGameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject parentHost;
    [SerializeField] private float duration = 5f;
    void OnEnable()
    {
        if(parentHost != null)
        {
            LeanTween.moveY(parentHost, parentHost.transform.position.y, duration).setFrom(parentHost.transform.position.y - 3f);
        }
    }
}
