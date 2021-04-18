using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingTwirl : MonoBehaviour
{
    [SerializeField] private GameObject[] boxy = new GameObject[2];
    [SerializeField] private float duration = 1f;
    void Start()
    {
        for(int i = 0; i < boxy.Length; i++)
        {
            LeanTween.rotateAround(boxy[i], Vector3.forward, 360f, duration).setLoopClamp();
        }
    }
}
