using System;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerDetector : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float radius = 5f;
    [SerializeField] private LayerMask _detectionMask = ~0;
    [SerializeField] private Transform _origin;
    
    [Header("Performance")]
    [SerializeField] private float _checkInterval = 0.15f;
    [SerializeField] private int _bufferSize = 16;

    private Collider2D[] _overlapBuffer;
    private WaitForSeconds _wait;
    private Coroutine _checkCoroutine;
    private bool _playerInZone;

    private bool _isInit;
    private bool _isActivate;
    
    public event Action<Transform> PlayerDetected;
    public event Action PlayerLost;
    
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
        if (_origin == null) 
            _origin = transform;
        
        _overlapBuffer = new Collider2D[Mathf.Max(1, _bufferSize)];
        _wait = new WaitForSeconds(Mathf.Max(0.01f, _checkInterval));
        
        _isInit = true;
    }

    public void Activate()
    {
        if (!_isInit)
            return;
        
        _checkCoroutine = StartCoroutine(CheckLoop());
        
        _isActivate = true;
    }

    public void Deactivate()
    {
        if (!_isInit)
            return;
        
        if (_checkCoroutine != null)
        {
            StopCoroutine(_checkCoroutine);
            _checkCoroutine = null;
        }
        
        _isActivate = false;
    }

    private IEnumerator CheckLoop()
    {
        while (true)
        {
            CheckOnce();
            yield return _wait;
        }
    }

    private void CheckOnce()
    {
        if (!_isActivate)
            return;
        
        int foundCount = Physics2D.OverlapCircleNonAlloc(_origin.position, radius, _overlapBuffer, _detectionMask);
        bool foundPlayer = false;
        
        Player player = null;

        for (int i = 0; i < foundCount; i++)
        {
            Collider2D col = _overlapBuffer[i];
            if (col == null) continue;
            if (col.TryGetComponent(out player) && player.gameObject.activeInHierarchy)
            {
                foundPlayer = true;
                break;
            }
        }

        if (foundPlayer && !_playerInZone)
        {
            _playerInZone = true;
            PlayerDetected?.Invoke(player.transform);
        }
        else if (!foundPlayer && _playerInZone)
        {
            _playerInZone = false;
            PlayerLost?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Transform t = _origin == null ? this.transform : _origin;
        Gizmos.color = _playerInZone ? new Color(0f, 1f, 0f, 0.25f) : new Color(1f, 0.5f, 0f, 0.15f);
        Gizmos.DrawWireSphere(t.position, radius);
        Gizmos.DrawSphere(t.position, 0.02f);
    }
}