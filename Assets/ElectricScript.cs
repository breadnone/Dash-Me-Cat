using System.Collections;
using UnityEngine;

public class ElectricScript : MonoBehaviour
{
    [SerializeField] private GameObject[] electrics =  new GameObject[3];
    [SerializeField] private float duration = 10f;
    private bool isMovin = false;
    private Vector3 cacheInitPos;
    void Start()
    {
        cacheInitPos = electrics[0].transform.position;
    }
    private int[] intArr = new int[]{0,1,2,3,4};
    public void StartObstaclesFour()
    {
        isMovin = true;
        StartCoroutine(AniamteElects());
        
        int randr = Random.Range(0, intArr.Length);
        if(randr == 1)
        {
            LeanTween.moveX(electrics[0], 1f, duration/2.5f).setFrom(-1f).setLoopPingPong().setIgnoreTimeScale(true);
        }
        if(randr == 2)
        {
            LeanTween.moveX(electrics[0], -1f, duration/2.5f).setFrom(1f).setLoopPingPong().setIgnoreTimeScale(true);
        }
        LeanTween.moveY(electrics[0], -6f, duration).setOnComplete(()=>
        {
            isMovin = false;
            electrics[0].transform.position = cacheInitPos;
            if(randr == 1 || randr == 2)
            {
                LeanTween.cancel(electrics[0]);
            }
        }).setIgnoreTimeScale(true);
    }
    WaitForSecondsRealtime waity = new WaitForSecondsRealtime(0.15f);
    private IEnumerator AniamteElects()
    {
        while(isMovin)
        {
            for(int i = 1; i < electrics.Length; i++)
            {
                electrics[i].SetActive(true);
                yield return waity;
                electrics[i].SetActive(false);
            }
        }
        yield break;
    }
}
