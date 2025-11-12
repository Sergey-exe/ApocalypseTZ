using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeBox : Item
{
    [SerializeField] private int _maxCountCartridge = 39;
    [SerializeField] private int _currentCountCartridge = 39;
    
    public override void Activate()
    {
        if (_currentCountCartridge > 0)
        {
            _currentCountCartridge--;
            return;
        }
        
        Consume();
        _currentCountCartridge = _maxCountCartridge;
    }
}