using UnityEngine;

public class ObstaclesFiveScript : MonoBehaviour
{
    [SerializeField] private GameObject[] ngo = new GameObject[3];
    [SerializeField] private float duration = 7f;
    // Start is called before the first frame update
    private Vector3 cachePos;
    private Vector3 cacheScale;
    private float cacheYpivotOne;
    private float cacheYpivotTwo;
    private int[] ranArr = new int[]{0,1,2,3,4,5};
    void Start()
    {
        cachePos = ngo[0].transform.position;
        cacheYpivotOne = ngo[1].transform.position.y;
        cacheYpivotTwo = ngo[2].transform.position.y;
    }
    public void ObstaclesFivee()
    {
        int randR = Random.Range(0, ranArr.Length);
        if(randR % 2 == 0)
        {
            LeanTween.moveX(ngo[0], -1f, 1.5f).setFrom(new Vector3(cachePos.x + 1f, cachePos.y, cachePos.z)).setLoopPingPong().setIgnoreTimeScale(true);
            LeanTween.moveY(ngo[0], cacheYpivotOne, duration).setIgnoreTimeScale(true).setOnComplete(()=>
            {
                LeanTween.cancel(ngo[0]);
                ngo[0].transform.position = cachePos;
            });
        }
        else
        {
            LeanTween.moveX(ngo[0], 1f, 1.5f).setFrom(new Vector3(cachePos.x - 1f, cachePos.y, cachePos.z)).setLoopPingPong().setIgnoreTimeScale(true);
            LeanTween.moveY(ngo[0], cacheYpivotTwo, duration).setIgnoreTimeScale(true).setOnComplete(()=>
            {
                LeanTween.cancel(ngo[0]);
                ngo[0].transform.position = cachePos;
            });
        }
    }
}
