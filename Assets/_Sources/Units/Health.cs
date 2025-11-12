using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _protection;
    [SerializeField] private float _percentProtection = 5;

    private bool _isInit;
    private bool _isActivate;

    public event UnityAction IsDeaded;
    public event UnityAction ChangeHealth;

    [field: SerializeField] public float CurrentHealth { get; private set; }

    [field: SerializeField] public float MaxHealth { get; private set; }
    
    public virtual  void Init()
    {
        CurrentHealth = MaxHealth;
        
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

    public virtual void TakeDamage(float damage)
    {
        if(!_isActivate)
            return;
        
        if (damage < 0)
            return;

        int percent = 100;

        float finalDamage = damage - (_protection / percent * _percentProtection);

        CurrentHealth -= finalDamage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        ChangeHealth?.Invoke();

        if (CurrentHealth <= 0)
            IsDeaded?.Invoke();
    }

    public virtual void TakeHeal(float heal)
    {
        if(!_isActivate)
            return;
        
        if (heal < 0)
            return;

        CurrentHealth += heal;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        ChangeHealth?.Invoke();
    }

    protected void SetHealth(float health)
    {
        if(health < 0)
            return;
        
        CurrentHealth = health;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        
        ChangeHealth?.Invoke();
    }
    
    protected void InvokeChangeHealth()
    {
        ChangeHealth?.Invoke();
    }
}
