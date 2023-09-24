using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI stateDebug;
        [SerializeField] private GameState currentState = GameState.SHOPPING;
        [SerializeField] private TextMeshProUGUI currencyText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Shop.Shop shop;
        [SerializeField] private GameObject pauseUI;
        [SerializeField] private PlayerInput input;

        public UnityEvent onChangeState;
        
        public GameState CurrentState => currentState;
        public int CurrentMoney => _currency;

        private GameState _previousState;
        private int _score;
        private int _currency = 10;

        private void Start()
        {
            shop.manager = this;
            pauseUI.SetActive(false);
            input.actions["Pause"].performed += TogglePause;

            currencyText.text = _currency.ToString();
            scoreText.text = _score.ToString();

#if !UNITY_EDITOR
            stateDebug.enabled = false;
#endif
            OnChangeState();
        }

        public void OnStartWave()
        {
            currentState = GameState.PLAYING;
            shop.HideUI();
            
            OnChangeState();
        }

        public void OnFinishWave()
        {
            currentState = GameState.SHOPPING;
            shop.EnableShopButtons();
            shop.ShowUI();
            
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
            PlayerPrefs.SetInt("Score", _score);
            scoreText.text = _score.ToString();
        }

        public void AddCurrency(int currency)
        {
            _currency += currency;
            currencyText.text = _currency.ToString();
        }

        public void SpendCurrency(int value)
        {
            _currency -= value;
            currencyText.text = _currency.ToString();
        }

        private void OnChangeState()
        {
            //input.SwitchCurrentActionMap(currentState != GameState.PLAYING ? "UI" : "Game");
            onChangeState.Invoke();
            stateDebug.text = currentState.ToString();
        }
    }
}
