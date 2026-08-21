using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleCarPhysics.Car
{
    public class WheelVisual : MonoBehaviour
    {
        [SerializeField] private WheelCollider _wheelCollider;

        private void Awake()
        {
            Assert.IsNotNull(_wheelCollider, nameof(_wheelCollider));
        }

        private void Update()
        {
            _wheelCollider.GetWorldPose(out var position, out var rotation);
            transform.SetPositionAndRotation(position, rotation);
        }
    }
}
