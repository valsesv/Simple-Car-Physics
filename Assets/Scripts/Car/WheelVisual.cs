using UnityEngine;

namespace SimpleCarPhysics.Car
{
    /// <summary>
    /// Places the wheel visual on the ground from its WheelCollider.
    /// Same idea as WheelPosition in the offroad sample.
    /// </summary>
    public class WheelVisual : MonoBehaviour
    {
        [SerializeField] private WheelCollider _wheelCollider;

        private void Awake()
        {
            if (_wheelCollider == null)
            {
                _wheelCollider = GetComponentInParent<WheelCollider>();
            }
        }

        private void Update()
        {
            if (_wheelCollider == null)
            {
                return;
            }

            var center = _wheelCollider.transform.TransformPoint(_wheelCollider.center);
            var maxDistance = _wheelCollider.suspensionDistance + _wheelCollider.radius;

            if (Physics.Raycast(center, -_wheelCollider.transform.up, out var hit, maxDistance))
            {
                transform.position = hit.point + (_wheelCollider.transform.up * _wheelCollider.radius);
            }
            else
            {
                transform.position =
                    center - (_wheelCollider.transform.up * _wheelCollider.suspensionDistance);
            }

            transform.Rotate(
                _wheelCollider.rpm / 60f * 360f * Time.deltaTime,
                0f,
                0f);
        }
    }
}
