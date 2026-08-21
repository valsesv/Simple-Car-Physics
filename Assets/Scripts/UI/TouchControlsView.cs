using System;
using SimpleCarPhysics.Gameplay;
using SimpleCarPhysics.Input;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SimpleCarPhysics.UI
{
    public class TouchControlsView : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _gasButton;
        [SerializeField] private Button _reverseButton;
        [SerializeField] private Button _restartButton;

        [Header("Systems")]
        [SerializeField] private UICarInput _uiInput;
        [SerializeField] private LevelRestarter _levelRestarter;

        private bool _gasHeld;
        private bool _reverseHeld;

        private void Awake()
        {
            Assert.IsNotNull(_gasButton, nameof(_gasButton));
            Assert.IsNotNull(_reverseButton, nameof(_reverseButton));
            Assert.IsNotNull(_restartButton, nameof(_restartButton));
            Assert.IsNotNull(_uiInput, nameof(_uiInput));
            Assert.IsNotNull(_levelRestarter, nameof(_levelRestarter));

            BindHold(_gasButton, pressed => _gasHeld = pressed);
            BindHold(_reverseButton, pressed => _reverseHeld = pressed);
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartPressed);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartPressed);
            _gasHeld = false;
            _reverseHeld = false;
            _uiInput.Clear();
        }

        private void Update()
        {
            _uiInput.SetForwardPressed(_gasHeld);
            _uiInput.SetReversePressed(_reverseHeld);
        }

        private void OnRestartPressed() => _levelRestarter.Restart();

        private static void BindHold(Button button, Action<bool> setHeld)
        {
            var trigger = button.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = button.gameObject.AddComponent<EventTrigger>();
            }

            Add(trigger, EventTriggerType.PointerDown, _ => setHeld(true));
            Add(trigger, EventTriggerType.PointerUp, _ => setHeld(false));
            Add(trigger, EventTriggerType.PointerExit, _ => setHeld(false));
        }

        private static void Add(EventTrigger trigger, EventTriggerType type, Action<BaseEventData> callback)
        {
            var entry = new EventTrigger.Entry { eventID = type };
            entry.callback.AddListener(callback.Invoke);
            trigger.triggers.Add(entry);
        }
    }
}
