using UnityEngine;

public class StandardHealthBar : HealthBar
{
    public override void ChangeBar()
    {
        float maxPercent = 100;
        float percentHealth;

        percentHealth = Health.CurrentHealth * maxPercent / Health.MaxHealth;
        percentHealth = Mathf.Round(percentHealth);

        Text.text = $"{percentHealth}%";
        Bar.value = percentHealth;
    }
}
