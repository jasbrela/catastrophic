using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Waves
{
    public class WaveController : MonoBehaviour
    {
        [SerializeField] private List<WaveData> waves = new();
        public WaveData CurrentWave => _current;
        private WaveData _current;

        private int currentIndex;

        private void Start()
        {
            _current = waves[currentIndex];
        }

        public void NextWave()
        {
            currentIndex++;
            if (waves.Count > currentIndex)
            {
                _current = waves[currentIndex];
            }
            else
            {
                Debug.Log("Finish");
            }
        }
    }
}
