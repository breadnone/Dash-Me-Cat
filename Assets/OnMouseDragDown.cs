
using UnityEngine;

public class OnMouseDragDown : MonoBehaviour
{
    [SerializeField] private Camera camx;
    [SerializeField] private Transform imageToMove;
    private float distance;

    void OnMouseDown()
    {
        distance = camx.WorldToScreenPoint(imageToMove.position).z;
    }

    void OnMouseDrag()
    {          
        imageToMove.position = camx.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
    }
}
