using System;
using UnityEngine;

namespace SimpleCarPhysics.Car
{
    public class CarFlipDetector : MonoBehaviour
    {
        [SerializeField] private float _flipDotThreshold = -0.05f;
        [SerializeField] private float _flipHoldSeconds = 0.45f;

        public event Action Flipped;

        private float _timer;
        private bool _fired;

        private void Update()
        {
            if (_fired)
            {
                return;
            }

            if (Vector3.Dot(transform.up, Vector3.up) >= _flipDotThreshold)
            {
                _timer = 0f;
                return;
            }

            _timer += Time.deltaTime;
            if (_timer < _flipHoldSeconds)
            {
                return;
            }

            _fired = true;
            Flipped?.Invoke();
        }
    }
}
