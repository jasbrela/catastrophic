using System.Collections;
using ScoreSpaceJam.Scripts.Entity.Enemy;
using ScoreSpaceJam.Scripts.Managers;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ScoreSpaceJam.Scripts.Waves
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI remainingEnemiesUI;
        [SerializeField] private GameManager manager;
        [SerializeField] private ObjectPool bulletPool;
        [SerializeField] private WaveController waveManager;
        [SerializeField] private Vector2 absBounds;
        [SerializeField] private float spawnRange;

        private int _killCount;
        private int _count;

        private void Start()
        {
            waveManager.onStartWave.AddListener(StartSpawning);
            remainingEnemiesUI.text = "";
        }

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
            Gizmos.DrawWireCube(center, new Vector3(absBounds.x * 2, absBounds.y * 2, 0));

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(center, new Vector3(absBounds.x * 2 + spawnRange * 2, absBounds.y * 2 + spawnRange * 2, 0));
        }

        private void StartSpawning()
        {
            _count = 0;
            _killCount = 0;

            StartCoroutine(Spawn());
            UpdateUI();
        }

        private IEnumerator Spawn()
        {
            while (_count < waveManager.CurrentWave.Size)
            {
                yield return new WaitForSeconds(waveManager.CurrentWave.Delay);

                var temp = 0;

                foreach (var waveEnemy in waveManager.CurrentWave.Enemies)
                {
                    temp += waveEnemy.quantity;
                    if (_count < temp)
                    {
                        Enemy enemy = Instantiate(waveEnemy.enemy, GetRandomPointOutsideCameraView(), transform.rotation);
                        enemy.RegisterGameManager(manager);

                        enemy.RegisterOnDeathEvent(OnEnemyKilled);

                        if (enemy is RangedEnemy)
                        {
                            (enemy as RangedEnemy).RegisterBulletPool(bulletPool);
                        }

                        _count++;
                        UpdateUI();
                        break;
                    }
                }
            }
        }

        private void OnEnemyKilled()
        {
            _killCount++;
            UpdateUI();

            if (_killCount == _count)
            {
                manager.OnFinishWave();
            }
        }

        private void UpdateUI()
        {
            remainingEnemiesUI.enabled = waveManager.CurrentWave.Size != _killCount;
            remainingEnemiesUI.text = $"{waveManager.CurrentWave.Size - _killCount} enemies remaining.";
        }
    }
}
