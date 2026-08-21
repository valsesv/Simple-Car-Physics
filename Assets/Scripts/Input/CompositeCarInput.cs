using UnityEngine;

namespace SimpleCarPhysics.Input
{
    public class CompositeCarInput : MonoBehaviour, ICarInput
    {
        [SerializeField] private MonoBehaviour[] _sources;

        public float Throttle { get; private set; }

        private void Update()
        {
            var value = 0f;
            if (_sources == null)
            {
                Throttle = 0f;
                return;
            }

            foreach (var source in _sources)
            {
                if (source is ICarInput carInput)
                {
                    var t = carInput.Throttle;
                    if (Mathf.Abs(t) > Mathf.Abs(value))
                    {
                        value = t;
                    }
                }
            }

            Throttle = Mathf.Clamp(value, -1f, 1f);
        }
    }
}
