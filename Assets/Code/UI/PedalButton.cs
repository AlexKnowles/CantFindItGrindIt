using UnityEngine;
using UnityEngine.EventSystems;


namespace CantFindItGrindIt.UI
{
    public class PedalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsBeingHeldDown { get; set; }

        void Awake()
        {
            IsBeingHeldDown = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsBeingHeldDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsBeingHeldDown = false;
        }
    }
}