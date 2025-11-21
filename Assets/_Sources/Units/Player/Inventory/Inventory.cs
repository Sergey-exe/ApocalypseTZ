using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const string SaveFileName = "InventorySave.json";
    
    [SerializeField] private List<Item> _items;
    [SerializeField] private InventorySelector _selector;
    [SerializeField] private ItemDestroyer _itemDestroyer;

    private InventorySave _save;
    private string _path;

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
        _save = new InventorySave();
        _selector.Init(_items);

        foreach (Item item in _items)
            item.Init();

        LoadSaves();

        _isInit = true;
    }

    public void Activate()
    {
        if (!_isInit)
            return;

        _itemDestroyer.Destroyed += Save;
        _selector.Activate();
        _isActivate = true;
    }

    public void Deactivate()
    {
        if (!_isInit)
            return;
        
        _itemDestroyer.Destroyed -= Save;
    }

    public void Add(ItemType type, int count)
    {
        if (!_isActivate)
            return;

        foreach (Item item in _items)
        {
            if (item.Type == type)
            {
                item.Add(count);
                Save();
                return;
            }
        }
    }

    public bool TryGetItem(ItemType type)
    {
        if (!_isActivate)
            throw new Exception("Попытка получения данных из не активированного класса!");

        foreach (var item in _items)
        {
            if (item.Type == type)
            {
                if (item.Count <= 0)
                    return false;

                item.Activate();
                Save();
                return true;
            }
        }

        return false;
    }
    
    public void Save()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        _path = Path.Combine(Application.dataPath, SaveFileName);
#endif
        _save.ItemCounts.Clear();

        foreach (var item in _items)
            _save.ItemCounts[item.Type] = item.Count;
        
        _save.OnBeforeSerialize();

        string json = JsonUtility.ToJson(_save, true);
        File.WriteAllText(_path, json);
    }

    private void LoadSaves()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, SaveFileName);
#else
        _path = Path.Combine(Application.dataPath, SaveFileName);
#endif
        if (!File.Exists(_path))
            return;

        string json = File.ReadAllText(_path);
        _save = JsonUtility.FromJson<InventorySave>(json);
        
        _save.OnAfterDeserialize();

        foreach (var item in _items)
        {
            if (_save.ItemCounts.TryGetValue(item.Type, out int savedCount))
                item.Add(savedCount);
        }
    }
}

public enum ItemType
{
    CartridgeBox,
    Apple,
    Banana,
}

[Serializable]
public class InventorySave : ISerializationCallbackReceiver
{
    public List<ItemSaveData> Items = new();

    [NonSerialized]
    public Dictionary<ItemType, int> ItemCounts = new();

    public void OnBeforeSerialize()
    {
        Items.Clear();
        foreach (var kvp in ItemCounts)
        {
            Items.Add(new ItemSaveData { Type = kvp.Key, Count = kvp.Value });
        }
    }

    public void OnAfterDeserialize()
    {
        ItemCounts.Clear();
        foreach (var data in Items)
        {
            ItemCounts[data.Type] = data.Count;
        }
    }
}

[Serializable]
public class ItemSaveData
{
    public ItemType Type;
    public int Count;
}
