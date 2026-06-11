using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Weapon/Effects/Dotdam")]


public class DotdamEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float dotDamage;
    float dotTime;
    float chargeAmount;
    public void GetCharge(float charge)
    {
        chargeAmount = charge;
    }
    public void Apply(GameObject target, float level)
    {


        EnemyMovement enemy = target.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            float finalDotDam = dotDamage * Mathf.Pow(1.15f, level);
            float finalDotTime = Mathf.Lerp(2, 8, chargeAmount);
            enemy.dotdam(finalDotDam, finalDotTime);
            Debug.Log("dotdam");
        }

    }



}
