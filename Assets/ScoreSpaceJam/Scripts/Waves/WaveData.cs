using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Waves
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WaveData")]
    public class WaveData : ScriptableObject
    {
        [Serializable]
        public class WaveEnemy
        {
            public GameObject enemy;
            public int quantity;
        }

        [SerializeField] private float delay = 1;
        [SerializeField] private List<WaveEnemy> enemies;

        public float Delay => delay;
        public List<WaveEnemy> Enemies => enemies;
        public int Size => _size;

        private int _size = 0;
        
        private void Awake()
        {
            foreach (WaveEnemy enemy in enemies)
            {
                _size += enemy.quantity;
            }
        }
    }
}
