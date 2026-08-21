using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleCarPhysics.Input
{
    public class KeyboardCarInput : MonoBehaviour, ICarInput
    {
        public float Throttle { get; private set; }

        private void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                Throttle = 0f;
                return;
            }

            var forward = keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed;
            var reverse = keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed;
            Throttle = forward == reverse ? 0f : (forward ? 1f : -1f);
        }
    }
}
