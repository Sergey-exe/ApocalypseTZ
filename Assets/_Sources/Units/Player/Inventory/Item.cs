using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Button ActivateButton;
    [SerializeField] protected UITextChanger TextChanger;

    private float _doubleClickThreshold = 0.3f;
    private Coroutine _clickCooldown;
    private WaitForSeconds _wait;
    
    public event Action<int> CountChanged;
    
    public int Count { get; private set; }
    
    [field: SerializeField] public ItemType Type { get; private set; }

    private void OnDestroy()
    {
        CountChanged -= TextChanger.ChangeInt;
        ActivateButton.onClick.RemoveListener(ActivateToDoubleClick);
    }

    public void Init()
    {
        CountChanged += TextChanger.ChangeInt;
        ActivateButton.onClick.AddListener(ActivateToDoubleClick);
    }

    public void ActivateToDoubleClick()
    {
        if (_clickCooldown == null)
        {
            _clickCooldown = StartCoroutine(ClickCooldown(_doubleClickThreshold));
            return;
        }
        
        Activate();
        StopCoroutine(_clickCooldown);
        _clickCooldown = null;
    }
    
    public abstract void Activate();

    public void Add(int count)
    {
        if(count <= 0)
            return;
        
        if(Count == 0)
            gameObject.SetActive(true);
        
        Count += count;
        CountChanged?.Invoke(Count);
    }

    public Button GetActivateButton()
    {
        return ActivateButton;
    }
    
    public void Clear()
    {
        for (int i = Count; i > 0; i--)
            Consume();
    }

    protected void Consume()
    {
        if (Count - 1 < 0)
            return;
        
        Count--;
        CountChanged?.Invoke(Count);
        
        if (Count == 0)
            gameObject.SetActive(false);
    }

    private IEnumerator ClickCooldown(float seconds)
    {
        _wait = new(seconds);
        
        yield return _wait;
        
        _clickCooldown = null;
    }
}