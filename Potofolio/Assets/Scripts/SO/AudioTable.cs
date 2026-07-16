using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public enum AudioType
{
    Button,
    SpecialBtn,
    NormalBtn,
    Base,
    Improved,
    Slime,
    Fire,
    Toxic,
    Energy,
    Bomb,
    Opening,
    Battle,
    SpaceShip,
    Upgrade,
    Unlock,
}
[System.Serializable]
public class AudioData
{
    public AudioType audioType;
    public AudioClip audioClip;
}


[CreateAssetMenu(menuName = "Data / AudioTable")]
public class AudioTable : ScriptableObject
{
    public List<AudioData> audioDatas = new();
}
