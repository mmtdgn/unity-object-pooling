using UnityEngine;

namespace MD.ObjectPooling
{
    public abstract class PooledObject : MonoBehaviour, IPooledObject
    {
        protected bool m_IsUsing;
        public bool IsUsing { get => m_IsUsing; set => m_IsUsing = value; }

        public void Reset()
        {
            gameObject.SetActive(false);
            m_IsUsing = false;
        }

        public void Init()
        {
            gameObject.SetActive(true);
            m_IsUsing = true;
        }
    }
}