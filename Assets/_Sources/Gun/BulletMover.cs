using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 15f;
    
    private bool _isActivate;
    
    public void Activate()
    {
        _isActivate = true;
    }
    
    private void Update()
    {
        if(!_isActivate)
            return;
        
        transform.position += transform.right * _moveSpeed * Time.deltaTime;
    }
}
