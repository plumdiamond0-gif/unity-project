using UnityEngine;



public class DamagaeItem : Item
{
    public override void Onuse()
    {
        playerAttack.Damage(57);
        Debug.Log("UseDamage");
        Destroy(gameObject);

    }
}
