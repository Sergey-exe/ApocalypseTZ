using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    
    public event Action<Bullet> Hit;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.TryGetComponent(out Health health))
            return;
        
        health.TakeDamage(_damage);
        Hit?.Invoke(this);
    }
}
