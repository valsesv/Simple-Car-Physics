using System;
using SimpleCarPhysics.Car;
using UnityEngine;

namespace SimpleCarPhysics.Gameplay
{
    public enum GameState
    {
        Playing,
        Won,
        Lost
    }

    public class GameController : MonoBehaviour
    {
        public GameState State { get; private set; } = GameState.Playing;

        public event Action<GameState> StateChanged;

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
            if (State == GameState.Playing)
            {
                SetState(GameState.Won);
            }
        }

        public void Lose()
        {
            if (State == GameState.Playing)
            {
                SetState(GameState.Lost);
            }
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
}
