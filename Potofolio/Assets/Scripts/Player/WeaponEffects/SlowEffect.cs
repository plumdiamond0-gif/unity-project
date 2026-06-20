using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(menuName = "Weapon/Effects/Slow")]


public class SlowEffect : ScriptableObject, IWeaponEffect
{
    [SerializeField] float slowTime;
    [SerializeField] float slowAmount;

    public void Apply(GameObject target, float level)
    {
        float fianlSlowTime = slowTime * Mathf.Pow(1.15f, level);
        float finalSlowAmount = slowAmount * Mathf.Pow(1.15f, level);

        EnemyMovement enemy = target.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.beSlow(fianlSlowTime, finalSlowAmount);
            Debug.Log("slow");
        }

    }


   
}
