using System.Collections.Generic;
using System;
using UnityEngine;

namespace MD.ObjectPooling
{
    public class ObjectPool : Singleton<ObjectPool>
    {
        [Serializable]
        public class Pool
        {
            public string ID;
            public Queue<PooledObject> PooledObjects;
            public int PoolSize;
            public GameObject ObjectPrefab;
        }

        [SerializeField] private Pool[] m_Pool = null;

        private GameObject m_PoolRoot;
        private GameObject[] m_SubRoots;

        private void Awake() => Init();

        private new void Init()
        {
            SpawnRoots();
            AddToPool();
        }

        private void SpawnRoots()
        {
            m_PoolRoot = new GameObject
            {
                name = "Poolx" + m_Pool.Length.ToString(),
                transform =
                {
                    position = Vector3.zero
                }
            };

            m_SubRoots = new GameObject[m_Pool.Length];

            for (var i = 0; i < m_SubRoots.Length; i++)
            {
                GameObject _SubRoot = new GameObject
                {
                    name = $"{m_Pool[i].ObjectPrefab.name.ToUpper()}_x{m_Pool[i].PoolSize.ToString()}",
                    transform =
                    {
                        position =Vector3.zero,
                        parent = m_PoolRoot.transform
                    }
                };
                m_SubRoots[i] = _SubRoot;
            }
        }

        private void AddToPool()
        {

            for (int j = 0; j < m_Pool.Length; j++)
            {
                m_Pool[j].PooledObjects = new Queue<PooledObject>();

                for (int i = 0; i < m_Pool[j].PoolSize; i++)
                {
                    SpawnObj(j);
                }
            }
        }

        private PooledObject SpawnObj(int j)
        {
            GameObject obj = Instantiate(m_Pool[j].ObjectPrefab,
            Vector3.down * 100f, Quaternion.identity, m_SubRoots[j].transform);

            obj.TryGetComponent(out PooledObject _pooledObj);
            if (_pooledObj == null)
                throw new Exception("Object does not have PooledObject component");

            _pooledObj.Reset();
            obj.name = $"{m_Pool[j].ID}_{m_Pool[j].PooledObjects.Count.ToString()}";

            m_Pool[j].PooledObjects.Enqueue(_pooledObj);
            return _pooledObj;
        }

        /// <summary>Get desired pool object at given position and rotation</summary>
        /// <param name="ID">Targeting pool ID</param>
        /// <param name="position">Spawn position</param>
        /// <param name="quaternion">Spawn rotation</param>
        /// <returns>Returns object as PooledObject type</returns>
        public PooledObject GetObject(string ID, Vector3 position, Quaternion quaternion)
        {
            int _PoolIndex = Array.FindIndex(m_Pool, x => x.ID == ID);

            if (_PoolIndex == -1)
            {
                throw new Exception("Pool ID Not Found Exception!");
            }

            PooledObject obj = null;

            if (m_Pool[_PoolIndex].PooledObjects.Peek().IsUsing)
            {
                obj = SpawnObj(_PoolIndex);
                m_Pool[_PoolIndex].PoolSize++;
                UpdatePoolName();
            }
            else
            {
                obj = m_Pool[_PoolIndex].PooledObjects.Dequeue();
                m_Pool[_PoolIndex].PooledObjects.Enqueue(obj);
            }

            obj.transform.position = position;
            obj.transform.rotation = quaternion;
            obj.Init();

            return obj;
        }

        /// <param name="ID">Pool ID</param>
        /// <returns>Returns targeting pool size with given ID.</returns>        
        public int GetPoolSize(String ID)
        {
            int _PoolIndex = Array.FindIndex(m_Pool, x => x.ID == ID);

            if (_PoolIndex == -1)
            {
                throw new Exception("Pool ID Not Found Exception!");
            }

            return m_Pool[_PoolIndex].PoolSize;
        }

        /// <param name="ID">Pool ID</param>
        /// <returns>Returns true if ID defined</returns>
        public bool Contains(string ID)
        {
            return Array.FindIndex(m_Pool, x => x.ID == ID) != -1;
        }

        /// <summary>Resets all objects</summary>
        public void ResetEntirePool()
        {
            List<PooledObject> _List;
            for (int i = 0; i < m_Pool.Length; i++)
            {
                _List = new List<PooledObject>(m_Pool[i].PooledObjects);
                for (int j = 0; j < _List.Count; j++)
                {
                    _List[j].Reset();
                }
                _List.Clear();
            }
        }

        private void UpdatePoolName()
        {
            for (int i = 0; i < m_SubRoots.Length; i++)
            {
                m_SubRoots[i].name = $"{m_Pool[i].ID}_x{m_Pool[i].PoolSize.ToString()}";
            }
        }
    }
}
