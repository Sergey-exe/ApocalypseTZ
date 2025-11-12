using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private float _attackRange = 1.5f;
    
    private Transform _target;

    private bool _isInit;
    private bool _isActivate;
    
    public bool NextToTarget { get; private set; }

    private void OnEnable()
    {
        if(!_isInit)
            return;
        
        _playerDetector.PlayerDetected += SetTarget;
        _playerDetector.PlayerLost += RemoveTarget;
    }
    
    private void OnDisable()
    {
        if(!_isInit)
            return;

        _playerDetector.PlayerDetected -= SetTarget;
        _playerDetector.PlayerLost -= RemoveTarget;
    }

    private void Update()
    {
        if(!_isActivate)
            return;
        
        Move();
    }

    public void Init()
    {
        _playerDetector.PlayerDetected += SetTarget;
        _playerDetector.PlayerLost += RemoveTarget;
        _isInit = true;
    }
    
    public void Activate()
    {
        if (!_isInit)
        {
            Debug.LogWarning("Попытка активации не инициализированного класса!");
            return;
        }
        
        _isActivate = true;
    }

    public Transform GetTarget()
    {
        return _target;
    }
    
    private void Move()
    {
        if(_target == null)
            return;
        
        Vector3 direction = (_target.position - transform.position);
        float distance = direction.magnitude;

        if(distance > _attackRange)
        {
            Vector3 move = direction.normalized * _moveSpeed * Time.deltaTime;
            
            if(move.magnitude > distance - _attackRange)
                move = direction.normalized * (distance - _attackRange);
            
            transform.position += move;
            
            NextToTarget = false;
        }
        else
        {
            NextToTarget = true;
        }

        if(Mathf.Abs(direction.x) > 0.01f)
        {
            float targetY = direction.x >= 0 ? 0f : 180f;
            transform.rotation = Quaternion.Euler(0f, targetY, 0f);
        }
    }

    private void SetTarget(Transform target)
    {
        _target = target;
    }

    private void RemoveTarget()
    {
        _target = null;
    }
}
