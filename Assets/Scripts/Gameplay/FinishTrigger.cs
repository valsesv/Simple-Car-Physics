using UnityEngine;

namespace SimpleCarPhysics.Gameplay
{
    public class FinishTrigger : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private string _vehicleTag = "Player";

        private void Awake()
        {
            if (_gameController == null)
            {
                _gameController = FindFirstObjectByType<GameController>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_gameController == null)
            {
                _gameController = FindFirstObjectByType<GameController>();
            }

            if (_gameController == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_vehicleTag) && !other.CompareTag(_vehicleTag))
            {
                return;
            }

            _gameController.Win();
        }
    }
}
