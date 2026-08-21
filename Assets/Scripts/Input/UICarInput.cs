using UnityEngine;

namespace SimpleCarPhysics.Input
{
    public class UICarInput : MonoBehaviour, ICarInput
    {
        private bool _forward;
        private bool _reverse;

        public float Throttle => _forward == _reverse ? 0f : (_forward ? 1f : -1f);

        public void SetForwardPressed(bool pressed) => _forward = pressed;

        public void SetReversePressed(bool pressed) => _reverse = pressed;

        public void Clear()
        {
            _forward = false;
            _reverse = false;
        }
    }
}
