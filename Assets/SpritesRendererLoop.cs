using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesRendererLoop : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject[] bcgs = new GameObject[2];
    [SerializeField] private float moveBcgDuration = 2f;
    [SerializeField] private bool flipXaxis = false;
    [SerializeField] private float flipXaxisDuration = 1f; 
    private Vector3 cachePos0;
    void OnEnable()
    {
        cachePos0 = bcgs[0].transform.position;
        //cachePos1 = bcgs[1].anchoredPosition;
        LoopBcg();
    }

    private void LoopBcg()
    {
        if(flipXaxis)
        {
            LeanTween.move(bcgs[0], bcgs[1].transform.position, moveBcgDuration).setLoopClamp();
            LeanTween.scaleX(bcgs[0], -1f, flipXaxisDuration).setLoopPingPong();
        }
        else
        {
            LeanTween.move(bcgs[0], bcgs[1].transform.position, moveBcgDuration).setLoopClamp();
        }
    }
}

