using UnityEngine;

public class SetActiveConfettis : MonoBehaviour
{
    [SerializeField] private GameObject[] confettis = new GameObject[15];
    [SerializeField] private float duration = 0.2f;
    private Vector3 bb = Vector3.one * 2f;
    private void OnEnable()
    {
        for(int i = 0; i < confettis.Length; i++)
        {
            confettis[i].transform.localScale = Vector3.zero;
        }
    }

    public void TriggerConfettis(Vector3 conPos)
    {
        for(int i = 0; i < confettis.Length; i++)
        {
            if(!LeanTween.isTweening(confettis[i]) && i % 1 == 0)
            {
                confettis[i].transform.position = new Vector3(conPos.x, conPos.y - 0.5f, conPos.z); 
                
                LeanTween.rotateAround(confettis[i], Vector3.forward, -360f, duration);
                LeanTween.scale(confettis[i], bb, duration).setOnComplete(
                    () =>
                    {
                        confettis[i].transform.localScale = Vector3.zero;
                    });
                return;
            }
        }
    }
}
