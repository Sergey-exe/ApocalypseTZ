using System;
using System.Collections;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _attackCooldown = 1f;

    private Transform _target;
    private Coroutine _attackCoroutine;
    
    private bool _isInit;
    private bool _isActivate;

    private void Update()
    {
        if (!_isActivate)
            return;
        
        if (!_mover.NextToTarget)
            return;

        _target = _mover.GetTarget();
        
        if (_target == null)
            return;
        
        if (_attackCoroutine == null)
            _attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    public void Init()
    {
        _isInit = true;
    }

    public void Activate()
    {
        if (!_isInit)
        {
            Debug.LogWarning("Попытка активации не инициализированного EnemyAttacker!");
            return;
        }
        
        _isActivate = true;
    }

    private IEnumerator AttackCoroutine()
    {
        if (_target.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }
        
        yield return new WaitForSeconds(_attackCooldown);
        
        _attackCoroutine = null;
    }
}