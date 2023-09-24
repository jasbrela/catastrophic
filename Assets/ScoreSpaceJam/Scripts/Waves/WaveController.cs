using System;
using System.Collections.Generic;
using ScoreSpaceJam.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ScoreSpaceJam.Scripts.Waves
{
    public class WaveController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentWaveText;
        [SerializeField] private GameManager manager;
        [SerializeField] private PlayerInput input;
        [SerializeField] private List<WaveData> waves = new();
        public UnityEvent onStartWave;
        
        public WaveData CurrentWave => _current;
        private WaveData _current;

        private int currentIndex = -1;

        private void Start()
        {
            input.actions["Ready"].performed += Ready;
        }

        private void Ready(InputAction.CallbackContext obj)
        {
            if (manager.CurrentState == GameState.SHOPPING)
            {
                NextWave();
            }
        }
        
        public void NextWave()
        {
            currentIndex++;
            if (waves.Count > currentIndex)
            {
                _current = waves[currentIndex];
                manager.OnStartWave();
                currentWaveText.text = (currentIndex + 1).ToString();
                onStartWave?.Invoke();
            }
            else
            {
                Debug.Log("Finish");
            }
        }
    }
}
