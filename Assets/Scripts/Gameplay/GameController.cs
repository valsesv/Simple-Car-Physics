using SimpleCarPhysics.Car;
using UnityEngine;

namespace SimpleCarPhysics.Gameplay
{
    public class GameController : MonoBehaviour
    {
        public GameState State { get; private set; } = GameState.Playing;

        public event System.Action<GameState> StateChanged;

        private void Awake()
        {
            Time.timeScale = 1f;
        }

        private void Start()
        {
            foreach (var detector in FindObjectsByType<CarFlipDetector>())
            {
                detector.Flipped += Lose;
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

            if (state != GameState.Playing)
            {
                Time.timeScale = 0f;
            }
        }
    }

    public enum GameState
    {
        Playing,
        Won,
        Lost
    }
}
