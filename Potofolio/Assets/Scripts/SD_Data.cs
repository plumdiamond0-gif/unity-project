using UnityEditor.Overlays;
using UnityEngine;

[System.Serializable] //Á÷·ÄÈ­
public class SD_User : ISaveData
{
    public ulong level;
    public ulong RedSlime;
    public ulong BlueSlime;
    public ulong Hp;



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
