using SimpleCarPhysics.Gameplay;
using SimpleCarPhysics.Input;
using UnityEngine;
using UnityEngine.Assertions;

namespace SimpleCarPhysics.Car
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private MonoBehaviour _inputSource;

        [Header("Input")]
        [SerializeField] private bool _invertDrive;
        [SerializeField] private bool _invertPitch;

        [Header("Drive")]
        [SerializeField] private float _driveForce = 5000f;
        [SerializeField] private float _pitchTorque = 12f;
        [SerializeField] private Vector3 _centerOfMass = new Vector3(0f, -0.15f, 0f);

        [Header("One axis modifier")]
        [SerializeField] private float _wheelieDriveScale = 0.35f;
        [SerializeField] private float _wheeliePitchScale = 2.5f;

        private WheelCollider[] _wheels;
        private ICarInput _input;
        private GameController _game;
        private float _throttle;
        private bool _locked;

        private void Awake()
        {
            Assert.IsNotNull(_rigidbody, nameof(_rigidbody));
            Assert.IsNotNull(_inputSource, nameof(_inputSource));
            _input = _inputSource as ICarInput;
            Assert.IsNotNull(_input, nameof(_input));
            _wheels = GetComponentsInChildren<WheelCollider>(true);
            Assert.IsTrue(_wheels.Length > 0, nameof(_wheels));
            _game = FindAnyObjectByType<GameController>();
            Assert.IsNotNull(_game, nameof(_game));
            _rigidbody.centerOfMass = _centerOfMass;
        }

        private void OnEnable()
        {
            _game.StateChanged += OnGameStateChanged;
        }

        private void OnDisable()
        {
            _game.StateChanged -= OnGameStateChanged;
        }

        private void Update()
        {
            if (_locked)
            {
                _throttle = 0f;
                return;
            }

            var value = Mathf.Clamp(_input.Throttle, -1f, 1f);
            _throttle = _invertDrive ? -value : value;
        }

        private void FixedUpdate()
        {
            if (_locked || Mathf.Abs(_throttle) < 0.01f)
            {
                return;
            }

            GetAxleContact(out var front, out var rear);
            var wheelie = front != rear;
            var driveScale = wheelie ? _wheelieDriveScale : 1f;
            var pitchScale = wheelie ? _wheeliePitchScale : 1f;
            var pitch = _invertPitch ? _throttle : -_throttle;

            _rigidbody.AddForce(transform.forward * (_throttle * _driveForce * driveScale));
            _rigidbody.AddTorque(transform.right * (pitch * _pitchTorque * pitchScale), ForceMode.Acceleration);
        }

        private void GetAxleContact(out bool front, out bool rear)
        {
            front = false;
            rear = false;

            foreach (var wheel in _wheels)
            {
                if (!wheel.isGrounded)
                {
                    continue;
                }

                if (transform.InverseTransformPoint(wheel.transform.position).x >= 0f)
                {
                    front = true;
                }
                else
                {
                    rear = true;
                }
            }
        }

        private void OnGameStateChanged(GameState state)
        {
            _locked = state != GameState.Playing;
        }
    }
}
