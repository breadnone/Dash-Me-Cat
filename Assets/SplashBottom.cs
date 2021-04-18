using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBottom : MonoBehaviour
{
    
    [SerializeField] private GameObject[] splasher = new GameObject[3];
    [SerializeField] private float duration = 3f;

    // Update is called once per frame
    void OnEnable()
    {
        LeanTween.moveY(splasher[0], 10f, duration).setLoopPingPong(1);
        LeanTween.moveX(splasher[0], 5f, duration);

        LeanTween.moveY(splasher[1], -10f, duration).setLoopPingPong(1);
        LeanTween.moveX(splasher[1], -5f, duration);
    }
}
