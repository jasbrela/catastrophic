using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameState currentState = GameState.PLAYING;
        [SerializeField] private PlayerInput input;
        
        public GameState CurrentState => currentState;

        private void Start()
        {
            input.actions["Pause"].performed += TogglePause;
        }

        public void StartGame()
        {
            currentState = GameState.PLAYING;
        }

        public void TogglePause(InputAction.CallbackContext ctx)
        {
            if (currentState == GameState.PAUSED)
            {
                currentState = GameState.PLAYING;
            }
            else if (currentState == GameState.PLAYING)
            {
                currentState = GameState.PAUSED;
            }
        }
    }
}
