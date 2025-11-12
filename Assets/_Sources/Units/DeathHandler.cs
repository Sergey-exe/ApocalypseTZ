using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Health _health;
    
    private int _deathHash = Animator.StringToHash("Death");
    private int _idleHash = Animator.StringToHash("Blend Tree");

    private bool _isInit;
    private bool _isActivate;
    
    public event Action OnDeathAnimationFinished;

    private void OnEnable()
    {
        if(!_isInit)
            return;
        
        _health.IsDeaded += OnDead;
    }

    private void OnDisable()
    {
        if(!_isInit)
            return;
        
        _health.IsDeaded -= OnDead;
    }

    public void Init()
    {
        _health.IsDeaded += OnDead;
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

    private void OnDead()
    {
        if (!_isActivate)
            return;
        
        _animator.SetTrigger(_deathHash);
        StartCoroutine(HandleDeathAnimation());
    }

    private IEnumerator HandleDeathAnimation()
    {
        yield return new WaitForSeconds(GetDeathAnimationLength());
        
        _animator.ResetTrigger(_deathHash);
        _animator.Play(_idleHash, 0, 0f);
        OnDeathAnimationFinished?.Invoke();
    }

    private float GetDeathAnimationLength()
    {
        RuntimeAnimatorController ac = _animator.runtimeAnimatorController;
        
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "Death")
                return ac.animationClips[i].length;
        }
        return 1f;
    }
}