using UnityEngine;
using MD.ObjectPooling;

public class Projectile : PooledObject
{
    [SerializeField] private float m_Speed;
    
    private void Update()
    {
        if (!m_IsUsing) return;
        transform.position += transform.forward * Time.deltaTime * m_Speed;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IEnemy enemy))
        {
            //enemy.TakeDamage();
           Reset();
        }
    }
}