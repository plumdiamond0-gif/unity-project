using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

[System.Serializable] //Á÷·ÄÈ­
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
        {OutItemType.MonsterCoin, 0},
        {OutItemType.RedJelly, 0},
        {OutItemType.SlimeShell, 0},
        {OutItemType.SporeSac, 0},
        {OutItemType.WatchersEye, 0},
        {OutItemType.GaleCrystal, 0},
        {OutItemType.EmeraldShard, 0},
        {OutItemType.ToxicThornFragment, 0},
        {OutItemType.FlameCrystal, 0},
        {OutItemType.HardenedFang, 0},
        {OutItemType.KnightsEmblem, 0},
        {OutItemType.ManaCrystal, 0},
        {OutItemType.None, 0},


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
    
        
    

    //public string GetSaveKey()
    //{
    //    return nameof(SD_User);
    //}

    public void CloneCopy(SD_User userData)
    {
        weaponActive = userData.weaponActive;   
        weaponlevel = userData.weaponlevel;
        itemStates = userData.itemStates;   

    }
}
