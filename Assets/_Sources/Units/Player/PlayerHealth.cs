using System.IO;
using UnityEngine;

public class PlayerHealth : Health
{
    private const string SaveFileName = "PlayerHealthSave.json";
    private string _path;

    public override void Init()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, SaveFileName);
#else
        _path = Path.Combine(Application.dataPath, SaveFileName);
#endif
        base.Init();
        LoadHealth();
        InvokeChangeHealth();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        SaveHealth();
    }

    public override void TakeHeal(float heal)
    {
        base.TakeHeal(heal);
        SaveHealth();
    }

    private void SaveHealth()
    {
        var save = new HealthSave { CurrentHealth = CurrentHealth };
        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(_path, json);
    }

    private void LoadHealth()
    {
        if (!File.Exists(_path))
            return;

        string json = File.ReadAllText(_path);
        HealthSave save = JsonUtility.FromJson<HealthSave>(json);
        
        SetHealth(save.CurrentHealth);
    }

    private void OnApplicationQuit()
    {
        SaveHealth();
    }
}

[System.Serializable]
public class HealthSave
{
    public float CurrentHealth;
}