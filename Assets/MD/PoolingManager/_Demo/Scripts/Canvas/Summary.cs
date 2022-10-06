using UnityEngine;
using TMPro;
using System;
using MD.ObjectPooling;

public class Summary : MonoBehaviour
{
    [SerializeField] private TMP_Text m_ObjectCounter;
    [SerializeField] private TMP_Text m_Cooldown;
    [SerializeField] private string ID;
    private ObjectPool m_ObjectPool;

    private void Awake()
    {
        m_ObjectPool = ObjectPool.Instance;
    }
    
    private void Update()
    {
        UpdateCountDisplayer();
    }

    public void IncreaseSpeed(Shooter shooter)
    {
        shooter.CooldownChangeAmount = -0.01f;
        UpdateSpeedDisplayer(shooter);
    }

    public void DecreaseSpeed(Shooter shooter)
    {
        shooter.CooldownChangeAmount = 0.01f;
        UpdateSpeedDisplayer(shooter);
    }

    private void UpdateCountDisplayer()
    {
        m_ObjectCounter.text = $"Object Count :{m_ObjectPool.GetPoolSize(ID).ToString()}";
    }
    
    private void UpdateSpeedDisplayer(Shooter shooter)
    {
        m_Cooldown.text = $"CD: {String.Format("{0:0.00}", shooter.GetFireCooldown)}s";
    }
}