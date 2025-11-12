using System;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public event Action<Bonus> Upped;
    
    [field: SerializeField] public ItemType Type { get; private set; }

    public void Up()
    {
        Upped?.Invoke(this);
    }
}