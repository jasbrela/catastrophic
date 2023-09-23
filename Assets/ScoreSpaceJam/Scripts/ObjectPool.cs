using System.Collections.Generic;
using UnityEngine;

namespace ScoreSpaceJam.Scripts
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private bool initializeAtStart = false;
        [SerializeField] private GameObject prefab;
        [SerializeField] private int size;
    
        private List<GameObject> pool = new();

        private void Start()
        {
            if (initializeAtStart) InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < size; i++)
            {
                CreateObject();
            }
        }

        private GameObject CreateObject()
        {
            GameObject go = Instantiate(prefab);
            go.SetActive(false);
            pool.Add(go);
            return go;
        }

        public GameObject GetObject()
        {
            foreach (GameObject go in pool)
            {
                if (go.activeInHierarchy) continue;
            
                go.SetActive(true);
                return go;
            }

            if (pool.Count < size)
            {
                return CreateObject();
            }
        
            return null;
        }

        public void ReturnGameObject(GameObject go)
        {
            go.SetActive(false);
        }
    }
}
