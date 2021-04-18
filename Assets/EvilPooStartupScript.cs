using UnityEngine;

public class EvilPooStartupScript : MonoBehaviour
{
    [SerializeField] private GameObject[] evilPoo = new GameObject[3];
    // Start is called before the first frame update
    private Vector3 cachePos;
    void OnEnable()
    {
        evilPoo[0].transform.position = evilPoo[1].transform.position;
    }
    void Start()
    {
        PooPoo();
    }

    private void PooPoo()
    {
        LeanTween.move(evilPoo[0], new Vector3(evilPoo[0].transform.position.x, evilPoo[0].transform.position.y + 570f, 0f), 2f).setEaseOutQuad().setOnComplete(
            () =>
            {
                evilPoo[3].SetActive(true);
                LeanTween.move(evilPoo[0], new Vector3(evilPoo[0].transform.position.x, evilPoo[0].transform.position.y + 40f, 0f), 0.2f).setDelay(0.5f).setLoopPingPong(2);
                LeanTween.move(evilPoo[0], evilPoo[1].transform.position, 0.4f).setDelay(1.4f).setEaseInBack().setOnComplete(
                    () =>
                    {
                        for(int i = 0; i < evilPoo.Length; i++)
                        {
                            LeanTween.cancel(evilPoo[i]);
                            evilPoo[i].SetActive(false);
                        }
                    });
            });
    }
}
