using UnityEngine;

public class HealItem : Item
{
    public Health PlayerHealth;

    public override void Onuse()
    {
        PlayerHealth.Heal(50);
        Debug.Log("UseHealth");
        Destroy(gameObject);

    }
}

