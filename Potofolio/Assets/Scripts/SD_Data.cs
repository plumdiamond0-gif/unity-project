using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

[System.Serializable] //Á÷·ÄÈ­
public class SD_User : ISaveData
{
    public ulong level;
    public ulong RedSlime;
    public ulong BlueSlime;
    public ulong Hp;




    public Dictionary<OutItemType, int> itemStates = new()
    {
        {OutItemType.MonsterCore, 0},
        {OutItemType.RedJelly, 0},
        {OutItemType.SlimeShell, 0},
        {OutItemType.SporeSac, 0},
        {OutItemType.WatchersEye, 0},
        {OutItemType.GaleCrystal, 0},
        {OutItemType.EmeraldShard, 0},
        {OutItemType.FlameCrystal, 0},
        {OutItemType.HardenedFang, 0},
        {OutItemType.KnightsEmblem, 0},
        {OutItemType.ManaCrystal, 0},
        {OutItemType.None, 0},


    };


    public Dictionary<WeaponState, int> weaponlevel = new()
    {
        {WeaponState.Base, 0 },
        {WeaponState.Improved, 0 },
        {WeaponState.Slime, 0 },
        {WeaponState.Toxic, 0 },
        {WeaponState.Fire, 0 },
        {WeaponState.Hypnosis, 0 },
        {WeaponState.Energy, 0 },
        {WeaponState.Bomb, 0 },

    };
    
        
    

    public string GetSaveKey()
    {
        return nameof(SD_User);
    }

    public void CloneCopy(SD_User userData)
    {
        RedSlime = userData.RedSlime;
        level = userData.level;
        BlueSlime = userData.BlueSlime;
    }
}
