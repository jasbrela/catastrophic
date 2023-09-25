using System.Collections;
using System.Collections.Generic;
using ScoreSpaceJam.Scripts.Entity.Enemy;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Waves
{
    [CreateAssetMenu(menuName = "ScriptableObjects/WaveData")]
    public class AllEnemiesData : MonoBehaviour
    {
        public List<Enemy> allEnemies = new();
    }
}
