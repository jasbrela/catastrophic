using System.Collections;
using NaughtyAttributes;
using ScoreSpaceJam.Scripts.Utils;
using UnityEngine;

namespace ScoreSpaceJam.Scripts.Waves
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private WaveController waveManager;
        [SerializeField] private Vector2 absBounds;
        [SerializeField] private float spawnRange;

        private int _count;

        private Vector3 GetRandomPointOutsideCameraView()
        {
            float x = Random.Range(-absBounds.x - spawnRange, absBounds.x + spawnRange);
            float y = Random.Range(-absBounds.y - spawnRange, absBounds.y + spawnRange);
            
            if (x > -absBounds.x && x < absBounds.x && y > -absBounds.y && y < absBounds.y)
            {
                if (Mathf.Abs(x - absBounds.x) < absBounds.x / 2) // is closer to right?
                {
                    x = absBounds.x + Random.Range(2, spawnRange);
                }
                else
                {
                    x = -absBounds.x - Random.Range(2, spawnRange);
                }
                
                if (Mathf.Abs(y - absBounds.y) < absBounds.y / 2) // is closer to up?
                {
                    y = absBounds.y + Random.Range(2, spawnRange);
                }
                else
                {
                    y = -absBounds.y - Random.Range(2, spawnRange);
                }
            }
            return new Vector3(x, y, 0);
        }
        
        private void OnDrawGizmos()
        {
            var center = transform.position;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, new Vector3(absBounds.x*2, absBounds.y*2, 0));
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, new Vector3(absBounds.x*2 + spawnRange*2, absBounds.y*2 + spawnRange*2, 0));
        }
        
        [Button]
        public void StartSpawning()
        {
            StartCoroutine(Spawn());
        }

        [Button]
        public void ForceSpawn()
        {
            Instantiate(waveManager.CurrentWave.Enemies[0].enemy, GetRandomPointOutsideCameraView(), transform.rotation);
        }

        private IEnumerator Spawn()
        {
            while (_count < waveManager.CurrentWave.Size)
            {
                yield return new WaitForSeconds(waveManager.CurrentWave.Delay);
                
                //Debug.Log("[Enemy Spawner]".Bold() + " Count: " + _count);
                
                var temp = 0;
                foreach (var waveEnemy in waveManager.CurrentWave.Enemies)
                {
                    temp += waveEnemy.quantity;
                    if (_count < temp)
                    {
                        Instantiate(waveEnemy.enemy, GetRandomPointOutsideCameraView(), transform.rotation);
                        
                        _count++;
                        break;
                    }
                }
            }

            waveManager.NextWave();
            _count = 0;
        }
    }
}
