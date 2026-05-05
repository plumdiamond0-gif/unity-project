using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Weapon/Effects/Stun")]


public class StunEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float stunTime;
    public void Apply(GameObject target)
    {


        EnemyMovement enemy = target.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.beStun(stunTime);
            Debug.Log("stun");
        }

    }



}
