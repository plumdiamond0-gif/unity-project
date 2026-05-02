using UnityEngine;

public class HealItem : Item
{
    public Health PlayerHealth;

    public override void Onuse()
    {
        PlayerHealth.TakeHealth(26);
        Debug.Log("UseHealth");
        Destroy(gameObject);

    }
}

