
using UnityEngine;

public class CollideScript : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SetActiveConfettis confetti;
    [SerializeField] private GameObject godModeProjectile;
    [SerializeField] private BoxCollider2D boxCol;
    [SerializeField] private RotatingPow rorPow;
    private Vector3 confSpwnPos;
    private string redss;
    private string whitess;
    private string obsOne;
    private float spwnY;
    private string gdMod;
    private string donuts;
    private string powa;
    private string obsFourss;
    public bool activeGodMode = false;
    private Vector2 cacheColiderSize;
    private bool powaState = false;
    public bool PowaState { get { return powaState; } set { powaState = value; } }
    public void ResetColliderSize(bool state, int sizes)
    {
        if(sizes == 1)
        {
            cacheColiderSize = boxCol.size;
            boxCol.size = new Vector2(17.31454f, 17.31454f);
        }

        if(!state)
        {
            boxCol.size = cacheColiderSize;
        }
        godModeProjectile.SetActive(state);
        activeGodMode = state;
    }

    void OnEnable()
    {
        redss = "Redss";
        whitess = "Whitess";
        obsOne = "boxTrailsSSS-outline";
        gdMod = "ProjectileGodMOde";
        powa = "barbWire";
        donuts = "Donutss";
        obsFourss = "ObstaclesFour";
    }
    void OnTriggerEnter2D(Collider2D cols)
    {
        string objCol = cols.gameObject.name;
        
        if(!activeGodMode)
        {
            
            if(objCol.Equals(redss))
            {
                gameManager.CounterLogic(1);
                LeanTween.cancel(cols.gameObject);
                spwnY = gameManager.SpawnPoint[Random.Range(0, gameManager.SpawnPoint.Length)].transform.position.y;
                confSpwnPos = cols.gameObject.transform.position;
                cols.gameObject.SetActive(false);
                confetti.TriggerConfettis(confSpwnPos);                      
                cols.gameObject.transform.position = new Vector3(gameManager.SpawnPoint[0].position.x, spwnY, 0f);
            }        
            if(objCol.Equals(whitess) || objCol.Equals(donuts))
            {
                gameManager.ScreenShake();
                cols.gameObject.SetActive(false);
                gameManager.CounterLogic(0);
                LeanTween.cancel(cols.gameObject);                
                spwnY = gameManager.SpawnPoint[Random.Range(0, gameManager.SpawnPoint.Length)].transform.position.y;
                cols.gameObject.transform.position = new Vector3(gameManager.SpawnPoint[0].position.x, spwnY, 0f);
            }
            if(objCol.Equals(obsOne) || objCol.Equals(obsFourss))
            {
                gameManager.CounterLogic(0);
                gameManager.ScreenShake();
            }
            if(powaState)
            {
                if(objCol.Equals(powa))
                {
                    rorPow.ResetPos();
                    gameManager.ScreenShake();
                    gameManager.BonusTimer();
                    godModeProjectile.SetActive(true);
                    ResetColliderSize(true, 1);
                }
            }
        }
        else
        {
            if(objCol.Equals(redss))
            {
                gameManager.ScreenShake();
                spwnY = gameManager.SpawnPoint[Random.Range(0, gameManager.SpawnPoint.Length)].transform.position.y;
                confSpwnPos = cols.gameObject.transform.position;
                LeanTween.cancel(cols.gameObject);
                cols.gameObject.SetActive(false);
                confetti.TriggerConfettis(confSpwnPos);                      
                cols.gameObject.transform.position = new Vector3(gameManager.SpawnPoint[0].position.x, spwnY, gameManager.SpawnPoint[0].position.z);
                gameManager.CounterLogic(1);
            }
            if(objCol.Equals(whitess) || objCol.Equals(donuts))
            {
                gameManager.ScreenShake();
                spwnY = gameManager.SpawnPoint[Random.Range(0, gameManager.SpawnPoint.Length)].transform.position.y;
                confSpwnPos = cols.gameObject.transform.position;
                LeanTween.cancel(cols.gameObject);
                cols.gameObject.SetActive(false);
                confetti.TriggerConfettis(confSpwnPos);
                cols.gameObject.transform.position = new Vector3(gameManager.SpawnPoint[0].position.x, spwnY, gameManager.SpawnPoint[0].position.z);
                gameManager.CounterLogic(1);
            }
        }
    }
}
