using SimpleCarPhysics.Car;
using UnityEngine;

namespace SimpleCarPhysics.Gameplay
{
    public class GameController : MonoBehaviour
    {
        public GameState State { get; private set; } = GameState.Playing;

        public event System.Action<GameState> StateChanged;

        private void Start()
        {
            var flipDetector = FindFirstObjectByType<CarFlipDetector>();
            if (flipDetector != null)
            {
                flipDetector.Flipped += Lose;
            }
        }

        public void Win()
        {
            if (State != GameState.Playing)
            {
                return;
            }

            SetState(GameState.Won);
            Debug.Log("Level complete");
        }

        public void Lose()
        {
            if (State != GameState.Playing)
            {
                return;
            }

            SetState(GameState.Lost);
            Debug.Log("Failed — press R / Space to restart");
        }

        private void SetState(GameState state)
        {
            State = state;
            StateChanged?.Invoke(state);
        }
    }

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }
}
