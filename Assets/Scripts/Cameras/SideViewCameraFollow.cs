using UnityEngine;

namespace SimpleCarPhysics.Cameras
{
    public class SideViewCameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset = new Vector3(2f, 2.5f, -14f);
        [SerializeField] private float _smooth = 8f;
        [SerializeField] private bool _lookAtTarget = true;

        private void LateUpdate()
        {
            if (_target == null)
            {
                var car = GameObject.FindWithTag("Player");
                if (car != null)
                {
                    _target = car.transform;
                }
                else
                {
                    return;
                }
            }

            var desired = new Vector3(_target.position.x, _target.position.y, 0f) + _offset;
            transform.position = Vector3.Lerp(transform.position, desired, 1f - Mathf.Exp(-_smooth * Time.deltaTime));

            if (_lookAtTarget)
            {
                var lookPoint = _target.position + Vector3.right * 2f;
                transform.rotation = Quaternion.LookRotation(lookPoint - transform.position, Vector3.up);
            }
        }
    }
}
