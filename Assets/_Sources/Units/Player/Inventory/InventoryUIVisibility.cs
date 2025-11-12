using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIVisibility : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private GameObject _menageButton;
    [SerializeField] private InventorySelector _selector;
    [SerializeField] private Button _showInventoryButton;

    private bool _inventoryShowed;

    private void OnEnable()
    {
        _showInventoryButton.onClick.AddListener(ChangeInventory);
        _selector.OnSelect += ActivateMenageButton;
    }

    private void OnDisable()
    {
        _showInventoryButton.onClick.RemoveListener(ChangeInventory);
        _selector.OnSelect -= ActivateMenageButton;
    }

    private void ChangeInventory()
    {
        if (_inventoryShowed == false)
        {
            _inventory.SetActive(true);
            _inventoryShowed = true;
            return;
        }

        _menageButton.SetActive(false);
        _inventory.SetActive(false);
        _inventoryShowed = false;
    }

    private void ActivateMenageButton()
    {
        _menageButton.SetActive(true);
    }
}
