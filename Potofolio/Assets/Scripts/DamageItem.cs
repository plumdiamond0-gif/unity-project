using UnityEngine;



public class DamagaeItem : Item
{
    public PlayerAttack playerAttack;

    public override void Onuse()
    {
        playerAttack.DamageUpdate(57);
        Debug.Log("UseDamage");
        Destroy(gameObject);
        
    }
}
