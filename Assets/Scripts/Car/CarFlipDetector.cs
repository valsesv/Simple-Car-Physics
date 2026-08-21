using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleCarPhysics.Car
{
    public class CarFlipDetector : MonoBehaviour
    {
        [SerializeField] private string _gateTag = "Gate";

        public event Action Flipped;

        private bool _fired;

        private void Awake()
        {
            Assert.IsFalse(string.IsNullOrEmpty(_gateTag), nameof(_gateTag));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_fired || other.isTrigger || other.CompareTag(_gateTag))
            {
                return;
            }

            _fired = true;
            Flipped?.Invoke();
        }
    }
}
