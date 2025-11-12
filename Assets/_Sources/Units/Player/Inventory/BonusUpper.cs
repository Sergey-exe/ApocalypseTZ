using UnityEngine;

public class BonusUpper : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Bonus bonus))
        {
            _inventory.Add(bonus.Type, 1);
            bonus.Up();
        }
    }
}