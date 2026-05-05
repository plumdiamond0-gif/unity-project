using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Weapon/Effects/Dotdam")]


public class DotdamEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float dotDamage;
    [SerializeField] float dotTime;

    public void Apply(GameObject target)
    {


        EnemyMovement enemy = target.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.dotdam(dotDamage,dotTime);
            Debug.Log("dotdam");
        }

    }



}
