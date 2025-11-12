using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public abstract class HealthBar : MonoBehaviour
{
    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public TextMeshProUGUI Text { get; private set; }

    [field: SerializeField] public Slider Bar { get; private set; }

    private void OnEnable()
    {
        Health.ChangeHealth += ChangeBar;
    }

    private void OnDisable()
    {
        Health.ChangeHealth -= ChangeBar;
    }

    private void Start()
    {
        Bar = GetComponent<Slider>();
    }

    public abstract void ChangeBar();
}
