using UnityEngine;
using UnityEngine.Assertions;

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

            Assert.IsNotNull(_gameController, nameof(_gameController));
            Assert.IsFalse(string.IsNullOrEmpty(_vehicleTag), nameof(_vehicleTag));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsPlayer(other))
            {
                return;
            }

            _gameController.Win();
        }

        private bool IsPlayer(Collider other)
        {
            if (other.CompareTag(_vehicleTag))
            {
                return true;
            }

            return other.attachedRigidbody != null && other.attachedRigidbody.CompareTag(_vehicleTag);
        }
    }
}
