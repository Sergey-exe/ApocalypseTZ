using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemDestroyer : MonoBehaviour
{
    [SerializeField] private Button _destroyButton;
    [SerializeField] private InventorySelector _selector;
    
    private Item _selectedItem;
    
    public event Action Destroyed;

    private void OnEnable()
    {
        _selector.ItemSelected += SetSelectedItem;
        _destroyButton.onClick.AddListener(DestroyItem);
    }

    private void OnDisable()
    {
        _selector.ItemSelected -= SetSelectedItem;
        _destroyButton.onClick.RemoveListener(DestroyItem);
        _selectedItem = null;
    }

    public void SetSelectedItem(Item selectedItem)
    {
        _selectedItem = selectedItem;
    }

    private void DestroyItem()
    {
        if (_selectedItem == null)
            return;
        
        _selectedItem.Clear();
        Destroyed?.Invoke();
    }
}
