using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySelector : MonoBehaviour
{
    [SerializeField] private Color _selectedColor = Color.yellow;
    [SerializeField] private Color _defaultColor = Color.white;

    private List<Item> _items;
    private Item _selectedItem;

    private bool _isInit;
    private bool _isActivate;
    
    public event Action OnSelect;
    public event Action<Item> ItemSelected;
    public event Action Deselected;

    private void OnDistroy()
    {
        foreach (var item in _items)
        {
            item.GetActivateButton().onClick.RemoveListener(() => OnItemSelected(item));
        }
    }
    
    public void Init(List<Item> items)
    {
        _items = items ?? throw new ArgumentNullException(nameof(items));
        
        foreach (var item in _items)
        {
            item.GetActivateButton().onClick.AddListener(() => OnItemSelected(item));
        }
        
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

    private void OnItemSelected(Item item)
    {
        if(!_isActivate)
            return;

        ResetSelection();

        _selectedItem = item;

        var colors = _selectedItem.GetActivateButton().colors;
        colors.selectedColor = _selectedColor;
        _selectedItem.GetActivateButton().colors = colors;
        OnSelect?.Invoke();
        ItemSelected?.Invoke(item);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_selectedItem.GetActivateButton().gameObject);
    }

    private void ResetSelection()
    {
        if(!_isActivate)
            return;
        
        if (_selectedItem == null)
            return;

        var colors = _selectedItem.GetActivateButton().colors;
        colors.selectedColor = _defaultColor;
        _selectedItem.GetActivateButton().colors = colors;
        Deselected?.Invoke();

        _selectedItem = null;
    }
}
