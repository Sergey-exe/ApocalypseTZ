using UnityEngine;

public class BarDestroyer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Canvas _canvas;

    private void OnEnable()
    {
        _health.IsDeaded += DestroyBar;
    }

    private void OnDisable()
    {
        _health.IsDeaded -= DestroyBar;
    }

    private void DestroyBar()
    {
        Destroy(_canvas.gameObject);
    }
}
