using System.Collections.Generic;
using TMPro;
using UnityEditor.Overlays;
using UnityEngine;

[System.Serializable] 
public class SD_User 
{
    public string date;

    public Dictionary<WeaponState, bool> weaponActive = new()
    {
        {WeaponState.Base, true},
        {WeaponState.Improved, true},
        {WeaponState.Slime, true},
        {WeaponState.Fire, true},
        {WeaponState.Toxic, true},
        {WeaponState.Energy, true},
        {WeaponState.Bomb, true},
    };

    public Dictionary<OutItemType, int> itemStates = new()
    {
        {OutItemType.MonsterCoin, 80000},
        {OutItemType.RedJelly, 8000},
        {OutItemType.SlimeShell, 8000 },
        {OutItemType.SporeSac, 8000},
        {OutItemType.WatchersEye, 8000},
        {OutItemType.CactusSpike, 8000},
        {OutItemType.EmeraldShard, 8000},
        {OutItemType.ToxicThornFragment, 8000},
        {OutItemType.FlameCrystal, 8000},
        {OutItemType.HardenedFang, 8000},
        {OutItemType.KnightsEmblem, 8000},
        {OutItemType.ManaCrystal, 8000},
        {OutItemType.None, 8000},


    };

    public Dictionary<WeaponState, int> weaponlevel = new()
    {
        {WeaponState.Base, 1 },
        {WeaponState.Improved, 1 },
        {WeaponState.Slime, 1 },
        {WeaponState.Toxic, 1 },
        {WeaponState.Fire, 1 },
        {WeaponState.Energy, 1 },
        {WeaponState.Bomb, 1 },

    };
}
