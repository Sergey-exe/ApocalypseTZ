using UnityEngine;

[RequireComponent(typeof(Animator))]
public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speedMultiplier = 1f;

    private Vector2 _previousPosition;

    private bool _isInit;
    private bool _isActivate;

    public void Init()
    {
        _previousPosition = _rigidbody.position;
        
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


    private void Update()
    {
        if(!_isActivate)
            return;
        
        Vector2 velocity = (_rigidbody.position - _previousPosition) / Time.deltaTime;
        float speed = velocity.magnitude;

        _animator.SetFloat("Speed", speed);
        _animator.speed = Mathf.Clamp(speed * _speedMultiplier, 0.1f, 3f);

        _previousPosition = _rigidbody.position;
    }
}