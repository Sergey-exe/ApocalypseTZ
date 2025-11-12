using UnityEngine;

public class Heal : Item
{
    [SerializeField] private int _countSaturation = 5;
    [SerializeField] private Health _health;
    
    public override void Activate()
    {
        _health.TakeHeal(_countSaturation);
        
        Consume();
    }
}