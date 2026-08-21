using SimpleCarPhysics.Gameplay;
using SimpleCarPhysics.Input;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace SimpleCarPhysics.UI
{
    public class TouchControlsView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private HoldButton _gasButton;
        [SerializeField] private HoldButton _reverseButton;
        [SerializeField] private Button _restartButton;

        [Header("Systems")]
        [SerializeField] private UICarInput _uiInput;
        [SerializeField] private LevelRestarter _levelRestarter;

        private void Awake()
        {
            Assert.IsNotNull(_gasButton, nameof(_gasButton));
            Assert.IsNotNull(_reverseButton, nameof(_reverseButton));
            Assert.IsNotNull(_restartButton, nameof(_restartButton));
            Assert.IsNotNull(_uiInput, nameof(_uiInput));
            Assert.IsNotNull(_levelRestarter, nameof(_levelRestarter));
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartPressed);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartPressed);
            _uiInput.Clear();
        }

        private void Update()
        {
            _uiInput.SetForwardPressed(_gasButton.IsPressed);
            _uiInput.SetReversePressed(_reverseButton.IsPressed);
        }

        private void OnRestartPressed() => _levelRestarter.Restart();
    }
}
