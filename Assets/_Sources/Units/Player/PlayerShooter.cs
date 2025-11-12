using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Button _shotButton;

    private bool _isInit;
    private bool _isActivate;

    private void OnEnable()
    {
        if (!_isInit)
            return;
        
        _shotButton.onClick.AddListener(Shot);
    }

    private void OnDisable()
    {
        if (!_isInit)
            return;
        
        _shotButton.onClick.RemoveListener(Shot);
    }
    
    public void Init()
    {
        _shotButton.onClick.AddListener(Shot);
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

    public void Shot()
    {
        if (!_isActivate)
        {
            Debug.LogWarning("PlayerShooter не активирован!");
            return;
        }
        
        _gun.Shot();
    }
}
