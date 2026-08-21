using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleCarPhysics.Gameplay
{
    public class FallKillZone : MonoBehaviour
    {
        [SerializeField] private GameController _gameController;
        [SerializeField] private string _vehicleTag = "Player";

        private void Awake()
        {
            if (_gameController == null)
            {
                _gameController = FindAnyObjectByType<GameController>();
            }

            Assert.IsNotNull(_gameController, nameof(_gameController));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (string.IsNullOrEmpty(_vehicleTag) || other.CompareTag(_vehicleTag))
            {
                _gameController.Lose();
            }
        }
    }
}
