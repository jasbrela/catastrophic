using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameState currentState = GameState.SHOPPING;
        [SerializeField] private GameObject shoppingUI;
        [SerializeField] private PlayerInput input;
        
        public GameState CurrentState => currentState;

        private void Start()
        {
            input.actions["Pause"].performed += TogglePause;
        }

        public void OnStartWave()
        {
            currentState = GameState.PLAYING;
            shoppingUI.SetActive(false);
        }

        public void OnFinishWave()
        {
            currentState = GameState.SHOPPING;
            shoppingUI.SetActive(true);
        }

        public void GameOver()
        {
            currentState = GameState.GAME_OVER;
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
