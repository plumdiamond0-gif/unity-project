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
        {OutItemType.BlueSlime, 0},
        {OutItemType.RedSlime, 0},
    };


    public Dictionary<WeaponState, int> weaponStates = new()
    {
        {WeaponState.Pistol, 0 },
        {WeaponState.Cannon, 0 },
        {WeaponState.SlimeGun, 0 },
        {WeaponState.PulseGun, 0 },
        {WeaponState.MissileLauncher, 0 },



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
