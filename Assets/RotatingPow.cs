using UnityEngine;
using UnityEngine.UI;

public class RotatingPow : MonoBehaviour
{
    [SerializeField] private GameObject[] pow = new GameObject[2];
    [SerializeField] private float yDuration = 4f;
    [SerializeField] private float xDuration = 2f;
    [SerializeField] private GameManager gemManager;
    [SerializeField] private Image fillAmountBonus;
    [SerializeField] private CollideScript disableGodMod;
    private Vector3 cachePosPow;
    private float cachePosPowY;
    private bool isRunning = false;
    //public bool PowIsRunning { get { return isRunning; } set { isRunning = value; } }
    void OnEnable()
    {        
        cachePosPow = pow[0].transform.position;
        cachePosPowY = pow[1].transform.position.y;
    }

    public void ResetPos()
    {
        LeanTween.cancel(pow[0]);
        pow[0].transform.position = cachePosPow;
        isRunning = false;
        disableGodMod.PowaState = false;
    }
    public void Powa()
    {
        if(!isRunning)
        {
            disableGodMod.PowaState = true;
            isRunning = true;
            //LeanTween.rotateAround(pow[2], Vector3.forward, 360f, 0.5f).setIgnoreTimeScale(true).setLoopClamp();
            LeanTween.moveX(pow[0], 2.5f, xDuration).setEaseInOutQuad().setIgnoreTimeScale(true).setLoopPingPong();
            LeanTween.moveY(pow[0], cachePosPowY, yDuration).setIgnoreTimeScale(true).setOnComplete(
                        () =>
                        {
                            LeanTween.cancel(pow[0]);
                            //LeanTween.cancel(pow[2]);
                            
                            if(gemManager.BonusStages == true)
                            {
                                gemManager.BonusPowaActivate(false);
                                fillAmountBonus.fillAmount = 0;
                                //disableGodMod.ResetColliderSize(false, 0);
                            }
                            ResetPos();
                            
                        });
        }
    }
}
