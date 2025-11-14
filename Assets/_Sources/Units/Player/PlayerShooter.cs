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
        Activate();
    }

    private void OnDisable()
    {
        Deactivate();
    }
    
    public void Init()
    {
        _isInit = true;
    }
    
    
    public void Activate()
    {
        if (!_isInit)
            return;
        
        _shotButton.onClick.AddListener(Shot);
        
        _isActivate = true;
    }

    public void Deactivate()
    {
        if (!_isInit)
            return;
        
        _shotButton.onClick.RemoveListener(Shot);
        
        _isActivate = false;
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
