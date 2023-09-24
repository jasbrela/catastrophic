using System;
using System.Collections;
using ScoreSpaceJam.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace ScoreSpaceJam.Scripts
{
    public class Stopwatch : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private GameManager manager;
        [SerializeField] private float refresh = 0.1f;
        private float timePassed;

        private void Start()
        {
            StartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            while (manager.CurrentState != GameState.GAME_OVER)
            {
                while (manager.CurrentState != GameState.PLAYING) yield return null;
                yield return new WaitForSecondsRealtime(refresh);
                timePassed += refresh;
                timerText.text = FormatCurrentTime();
            }
        }

        private string FormatCurrentTime()
        {
            int minutes = (int)timePassed / 60;
            float seconds = timePassed % 60;
            int milliseconds = (int)((seconds - (int)seconds) * 10);
            
            return $"{minutes:D2}:{(int)seconds:00}.{milliseconds}";
        }
    
    }
}
