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
        {OutItemType.MonsterCoin, 260},
        {OutItemType.RedJelly, 260},
        {OutItemType.SlimeShell, 260 },
        {OutItemType.SporeSac, 260},
        {OutItemType.WatchersEye, 260},
        {OutItemType.GaleCrystal, 260},
        {OutItemType.EmeraldShard, 260},
        {OutItemType.ToxicThornFragment, 260},
        {OutItemType.FlameCrystal, 260},
        {OutItemType.HardenedFang, 260},
        {OutItemType.KnightsEmblem, 260},
        {OutItemType.ManaCrystal, 260},
        {OutItemType.None, 260},


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
