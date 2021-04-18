using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public class RotatingGridSaw : MonoBehaviour
{
    [SerializeField] private GameObject[] gridSaw = new GameObject[6];
    [SerializeField] private GameObject[] circleSaw = new GameObject[7];
    [SerializeField] private float duration = 2f;
    private bool modeOne = true;
    private bool modetwo = false;
    void Start()
    {
        cacheGridOne = gridSaw[2].transform.position;
        cacheGridTwo = gridSaw[5].transform.position;

        for (int i = 0; i < gridSaw.Length; i++)
        {
            gridSaw[i].SetActive(false);
        }
        for (int i = 0; i < circleSaw.Length; i++)
        {
            if (circleSaw[i] != null)
                circleSaw[i].SetActive(false);
        }

    }

    private void ShowGridSaw(int state)
    {
        if (state == 0)
        {
            for (int i = 0; i < gridSaw.Length; i++)
            {
                if (gridSaw[i] != null)
                    gridSaw[i].SetActive(true);
            }
        }
        if (state == 1)
        {
            for (int i = 0; i < circleSaw.Length; i++)
            {
                if (circleSaw[i] != null)
                    circleSaw[i].SetActive(true);
            }
        }
    }

    public void PlayGridSaw(int parm)
    {
        ShowGridSaw(parm);
        if (parm == 0)
        {
            AnimateGridSawOne();
        }
        if (parm == 1)
        {
            StartCoroutine(AnimateCircleSawOne());
        }
    }
    private Vector3 cacheGridOne;
    private Vector3 cacheGridTwo;
    private void AnimateGridSawOne()
    {

        LeanTween.rotateAround(gridSaw[0], Vector3.forward, 360f, duration / 2f).setLoopClamp().setIgnoreTimeScale(true);
        LeanTween.rotateAround(gridSaw[3], Vector3.forward, 360f, duration / 2f).setLoopClamp().setIgnoreTimeScale(true);
        LeanTween.move(gridSaw[0], cacheGridOne, duration).setEaseInOutQuad().setIgnoreTimeScale(true).setLoopPingPong();
        LeanTween.move(gridSaw[3], cacheGridTwo, duration).setEaseInOutQuad().setIgnoreTimeScale(true).setLoopPingPong();
    }
    private float[] randR = new float[] { 10f, 12, 15f, 18, 20f };
    private bool wasRotatingTwo = false;
    private Vector3 cacheInitPos;
    private Vector3 cacheInitPosTwo;

    private Vector3 cachePosThree;
    private Vector3 cachePosFour;
    private Vector3 cachePosFive;
    private Vector3 cachePosSix;
    private Vector3 cachePosSeven;
    private Vector3 cachePosEight;
    private IEnumerator AnimateCircleSawOne()
    {
        if (!wasRotatingTwo)
        {
            cacheInitPos = circleSaw[0].transform.position;
            cacheInitPosTwo = circleSaw[9].transform.position;

            cachePosThree = circleSaw[3].transform.position;
            cachePosFour = circleSaw[4].transform.position;
            cachePosFive = circleSaw[5].transform.position;
            cachePosSix = circleSaw[6].transform.position;
            cachePosSeven = circleSaw[7].transform.position;
            cachePosEight = circleSaw[8].transform.position;

            wasRotatingTwo = true;
            LeanTween.rotateAround(circleSaw[1], Vector3.forward, -360f, duration / 2f).setLoopClamp().setIgnoreTimeScale(true);
            //LeanTween.rotateAround(circleSaw[2], Vector3.forward, 360f, duration / 2f).setLoopClamp().setIgnoreTimeScale(true);
            LeanTween.rotateAround(circleSaw[10], Vector3.forward, 360f, duration / 2f).setLoopClamp().setIgnoreTimeScale(true);
            //LeanTween.rotateAround(circleSaw[11], Vector3.forward, -360f, duration / 2f).setLoopClamp().setIgnoreTimeScale(true);
        }

        LeanTween.move(circleSaw[0], cachePosThree, duration).setEaseInOutQuad().setOnComplete(
                () =>
                {
                    LeanTween.move(circleSaw[0], cachePosFour, duration).setEaseInOutQuad().setOnComplete(
                            () =>
                            {
                                LeanTween.move(circleSaw[0], cachePosFive, duration).setEaseInOutQuad().setIgnoreTimeScale(true);
                            }).setIgnoreTimeScale(true);
                }).setIgnoreTimeScale(true);

        LeanTween.move(circleSaw[9], cachePosSix, duration).setEaseInOutQuad().setOnComplete(
                    () =>
                    {
                        LeanTween.move(circleSaw[9], cachePosSeven, duration).setEaseInOutQuad().setOnComplete(
                                () =>
                                {
                                    LeanTween.move(circleSaw[9], cachePosEight, duration).setEaseInOutQuad().setOnComplete(
                                            () =>
                                            {
                                                LeanTween.cancel(circleSaw[0]);
                                                LeanTween.cancel(circleSaw[1]);
                                                LeanTween.cancel(circleSaw[3]);
                                                wasRotatingTwo = false;
                                                circleSaw[9].transform.position = cacheInitPosTwo;
                                                circleSaw[0].transform.position = cacheInitPos;
                                            }).setIgnoreTimeScale(true);
                                }).setIgnoreTimeScale(true);
                    }).setIgnoreTimeScale(true);

        float tmpFl = duration * 4 + Random.Range(0, randR.Length);
        yield return new WaitForSecondsRealtime(tmpFl);
        StartCoroutine(AnimateCircleSawOne());
    }
}
