using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stateDebug;
        [SerializeField] private GameState currentState = GameState.SHOPPING;
        
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject shoppingUI;
        [SerializeField] private GameObject pauseUI;
        
        [SerializeField] private PlayerInput input;

        private int _score;
        private GameState _previousState;
        
        public GameState CurrentState => currentState;

        private void Start()
        {
            pauseUI.SetActive(false);
            input.actions["Pause"].performed += TogglePause;

            scoreText.text = "0";

#if !UNITY_EDITOR
            stateDebug.enabled = false;
#endif
            stateDebug.text = currentState.ToString();
        }

        public void OnStartWave()
        {
            currentState = GameState.PLAYING;
            shoppingUI.SetActive(false);
            
            stateDebug.text = currentState.ToString();
        }

        public void OnFinishWave()
        {
            currentState = GameState.SHOPPING;
            shoppingUI.SetActive(true);
            
            stateDebug.text = currentState.ToString();
        }

        public void GameOver()
        {
            currentState = GameState.GAME_OVER;
            
            stateDebug.text = currentState.ToString();
        }
        
        public void TogglePause(InputAction.CallbackContext ctx)
        {
            if (currentState == GameState.PAUSED)
            {
                Resume();
            }
            else if (currentState is GameState.PLAYING or GameState.SHOPPING)
            {
                pauseUI.SetActive(true);
                _previousState = currentState;
                currentState = GameState.PAUSED;
            }
            
            stateDebug.text = currentState.ToString();
        }

        public void Resume()
        {
            pauseUI.SetActive(false);
            currentState = _previousState;
            
            stateDebug.text = currentState.ToString();
        }

        public void Score(int score)
        {
            _score += score;
            scoreText.text = _score.ToString();
        }
    }
}
