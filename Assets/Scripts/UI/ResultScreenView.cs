using SimpleCarPhysics.Gameplay;
using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleCarPhysics.UI
{
    public class ResultScreenView : MonoBehaviour
    {
        [SerializeField] private GameObject _resultScreen;
        [SerializeField] private GameObject _winText;
        [SerializeField] private GameObject _loseText;
        [SerializeField] private GameController _gameController;

        private void Awake()
        {
            if (_gameController == null)
            {
                _gameController = FindAnyObjectByType<GameController>();
            }

            Assert.IsNotNull(_resultScreen, nameof(_resultScreen));
            Assert.IsNotNull(_winText, nameof(_winText));
            Assert.IsNotNull(_loseText, nameof(_loseText));
            Assert.IsNotNull(_gameController, nameof(_gameController));

            _resultScreen.SetActive(false);
            _winText.SetActive(false);
            _loseText.SetActive(false);
        }

        private void OnEnable()
        {
            _gameController.StateChanged += OnStateChanged;
        }

        private void OnDisable()
        {
            _gameController.StateChanged -= OnStateChanged;
        }

        private void OnStateChanged(GameState state)
        {
            if (state == GameState.Playing)
            {
                return;
            }

            _resultScreen.SetActive(true);
            _winText.SetActive(state == GameState.Won);
            _loseText.SetActive(state == GameState.Lost);
        }
    }
}
