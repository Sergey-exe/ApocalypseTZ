using System.Collections;
using UnityEngine;

public class SmoothHealthBar : HealthBar
{
    private const int Factor = 5;

    private Coroutine _coroutine;

    public override void ChangeBar()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmoothlyChangeHealthBar());
    }

    private IEnumerator SmoothlyChangeHealthBar()
    {
        float maxPercent = 100;
        float percentHealth = Health.CurrentHealth * maxPercent / Health.MaxHealth;
        float barValue = Bar.value;

        percentHealth = Mathf.Round(percentHealth);

        Text.text = $"{percentHealth}%";

        while (Bar.value != percentHealth)
        {
            barValue = Mathf.Lerp(barValue, percentHealth, Factor * Time.deltaTime);
            Bar.value = barValue;

            yield return null;
        }
    }
}
