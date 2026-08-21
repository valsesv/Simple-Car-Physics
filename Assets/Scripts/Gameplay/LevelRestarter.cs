using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SimpleCarPhysics.Gameplay
{
    public class LevelRestarter : MonoBehaviour
    {
        private void Update()
        {
            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return;
            }

            if (keyboard.rKey.wasPressedThisFrame || keyboard.spaceKey.wasPressedThisFrame)
            {
                Restart();
            }
        }

        public void Restart()
        {
            var active = SceneManager.GetActiveScene();
            SceneManager.LoadScene(active.buildIndex);
        }
    }
}
