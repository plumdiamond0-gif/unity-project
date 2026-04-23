using UnityEngine;

public class HealItem : Item
{
    public override void Onuse()
    {
        PlayerHealth.TakeHealth(26);
        Debug.Log("UseHealth");
        Destroy(gameObject);

    }
}

