using UnityEngine;

public class LoseShower : MonoBehaviour
{
    [SerializeField] private LoseWindow _loseWindow;
    [SerializeField] private Health _health;
    [SerializeField] private TimeChanger _timeChanger;

    private void OnEnable()
    {
        _health.IsDeaded += Show;
    }

    private void OnDisable()
    {
        _health.IsDeaded -= Show;
    }

    private void Show()
    {
        _loseWindow.ShowWindow();
        _timeChanger.StopTime();
    }
}