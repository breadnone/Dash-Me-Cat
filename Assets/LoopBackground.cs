
using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    [SerializeField] private GameObject[] bcgs = new GameObject[3];
    [SerializeField] private float moveBcgDuration = 2f;
    [SerializeField] private bool flipYaxis = false;
    [SerializeField] private bool flipXaxis = false;
    [SerializeField] private float flipYaxisDuration = 1f;
    [SerializeField] private float flipXaxisDuration = 1f; 
    private Vector3 cachePos0;

    void OnEnable()
    {
        cachePos0 = bcgs[0].transform.position;
        LoopBcg();
    }

    private void LoopBcg()
    {
        Vector3 transs = bcgs[1].transform.position;
        if(!flipYaxis && !flipXaxis)
        {
            LeanTween.move(bcgs[0], transs, moveBcgDuration).setLoopClamp();
        }
        if(flipYaxis)
        {
            LeanTween.move(bcgs[0], transs, moveBcgDuration).setLoopClamp();
            LeanTween.scaleY(bcgs[0], -1f, flipYaxisDuration).setLoopPingPong();
        }
        if(flipXaxis)
        {
            LeanTween.move(bcgs[0], transs, moveBcgDuration).setLoopClamp();
            LeanTween.scaleX(bcgs[0], -1f, flipXaxisDuration).setLoopPingPong();
        }
    }
}
