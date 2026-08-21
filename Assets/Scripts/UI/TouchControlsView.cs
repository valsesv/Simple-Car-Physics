using SimpleCarPhysics.Gameplay;
using SimpleCarPhysics.Input;
using UnityEngine;

namespace SimpleCarPhysics.UI
{
    public class TouchControlsView : MonoBehaviour
    {
        [SerializeField] private UICarInput _uiInput;
        [SerializeField] private LevelRestarter _levelRestarter;

        private void Awake()
        {
            if (_uiInput == null)
            {
                _uiInput = FindFirstObjectByType<UICarInput>();
            }

            if (_levelRestarter == null)
            {
                _levelRestarter = FindFirstObjectByType<LevelRestarter>();
            }
        }

        public void OnForwardDown() => _uiInput?.SetForwardPressed(true);
        public void OnForwardUp() => _uiInput?.SetForwardPressed(false);
        public void OnReverseDown() => _uiInput?.SetReversePressed(true);
        public void OnReverseUp() => _uiInput?.SetReversePressed(false);
        public void OnRestartPressed() => _levelRestarter?.Restart();
    }
}
