using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private DeathHandler _deathHandler;
    
    public event Action<Enemy> IsKilled;
    public event Action<Transform> OnDeathSpawnPoint;

    private void OnEnable()
    {
        _deathHandler.OnDeathAnimationFinished += Kill;
    }

    private void OnDisable()
    {
        _deathHandler.OnDeathAnimationFinished -= Kill;
    }
    
    public void Kill()
    {
        IsKilled?.Invoke(this);
        OnDeathSpawnPoint?.Invoke(transform);
    }
}
