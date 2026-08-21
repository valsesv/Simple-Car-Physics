using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SimpleCarPhysics.UI
{
    public class HoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public UnityEvent OnPressed;
        public UnityEvent OnReleased;

        public void OnPointerDown(PointerEventData eventData) => OnPressed?.Invoke();

        public void OnPointerUp(PointerEventData eventData) => OnReleased?.Invoke();

        public void OnPointerExit(PointerEventData eventData) => OnReleased?.Invoke();
    }
}
