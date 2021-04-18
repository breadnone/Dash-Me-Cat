
using UnityEngine;

public class RotatingProjectile : MonoBehaviour
{
    [SerializeField] private bool godMode = false; 
    [SerializeField] private GameObject[] rotateProjectile = new GameObject[2];
    void Start()
    {
        if(!godMode)
        {
            //LeanTween.rotateAround(rotateProjectile[0], Vector3.left, -360f, 1f).setLoopClamp();
            LeanTween.rotateAround(rotateProjectile[1], Vector3.forward, -360f, 3f).setLoopClamp();
            //LeanTween.rotateAround(rotateProjectile[0], Vector3.forward, 360f, 2f).setLoopClamp().setRecursive(false);
        }
    }
    void OnEnable()
    {
        if(godMode)
        {
            //LeanTween.rotateAround(rotateProjectile[0], Vector3.left, -360f, 1f).setLoopClamp();
            LeanTween.rotateAround(rotateProjectile[1], Vector3.forward, -360f, 3f).setLoopClamp().setRecursive(false);
            LeanTween.rotateAround(rotateProjectile[0], Vector3.forward, 360f, 2f).setLoopClamp().setRecursive(false);
        }
    }
    void OnDisable()
    {
        if(godMode)
        {
            LeanTween.cancel(rotateProjectile[1]);
            LeanTween.cancel(rotateProjectile[0]);
        }
    }
}
