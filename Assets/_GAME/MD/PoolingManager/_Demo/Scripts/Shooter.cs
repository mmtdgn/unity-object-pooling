using System.Collections;
using UnityEngine;
using MD.ObjectPooling;

public class Shooter : MonoBehaviour
{
    protected ObjectPool m_ObjectPool;
    [SerializeField] private Transform m_Barrel;
    [SerializeField] private string ID;
    [SerializeField] private float m_FireCooldown;
    public float GetFireCooldown => m_FireCooldown;
    public float CooldownChangeAmount
    {
        set
        {
            m_FireCooldown -= value;
        }
    }

    private void Awake()
    {
        m_ObjectPool = ObjectPool.Instance;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(m_FireCooldown);
            Shoot();
        }
    }
    
    private void Shoot()
    {
        m_ObjectPool.GetObject(ID, m_Barrel.position, m_Barrel.rotation);
    }
}
