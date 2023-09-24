using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stateDebug;
        [SerializeField] private GameState currentState = GameState.SHOPPING;
        [SerializeField] private TextMeshProUGUI currencyText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject shoppingUI;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private PlayerInput input;
        
        public GameState CurrentState => currentState;

        private GameState _previousState;
        private int _score;
        private int _currency;

        private void Start()
        {
            pauseUI.SetActive(false);
            input.actions["Pause"].performed += TogglePause;

            currencyText.text = "0";
            scoreText.text = "0";

#if !UNITY_EDITOR
            stateDebug.enabled = false;
#endif
            OnChangeState();
        }

        public void OnStartWave()
        {
            currentState = GameState.PLAYING;
            shoppingUI.SetActive(false);
            
            OnChangeState();
        }

        public void OnFinishWave()
        {
            currentState = GameState.SHOPPING;
            shoppingUI.SetActive(true);
            
            OnChangeState();
        }

        public void GameOver()
        {
            currentState = GameState.GAME_OVER;
            
            OnChangeState();
        }

        private void TogglePause(InputAction.CallbackContext ctx)
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
            
            OnChangeState();
        }

        public void Resume()
        {
            pauseUI.SetActive(false);
            currentState = _previousState;
            
            OnChangeState();
        }

        public void Score(int score)
        {
            _score += score;
            scoreText.text = _score.ToString();
        }

        public void AddCurrency(int currency)
        {
            _currency += currency;
            currencyText.text = currency.ToString();
        }

        private void OnChangeState()
        {
            //input.SwitchCurrentActionMap(currentState != GameState.PLAYING ? "UI" : "Game");
            
            stateDebug.text = currentState.ToString();
        }
    }
}
