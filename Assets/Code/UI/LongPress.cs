using UnityEngine;
using UnityEngine.EventSystems;

public class LongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool IsDown { get; private set; }
    private float downTime;

    public void OnPointerDown(PointerEventData eventData)
    {
        this.IsDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.IsDown = false;
    }

    void Update()
    {
    }

}