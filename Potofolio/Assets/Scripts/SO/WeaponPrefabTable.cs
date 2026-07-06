using UnityEngine;
using System.Collections.Generic;

public enum WeaponState
{
    Base,
    Improved,
    Slime,
    Toxic,
    Fire,
    Energy,
    Bomb,
}
[System.Serializable]
public class WeaponPrefabData
{


    public WeaponState weaponState;
    //public string WeaponName;
    public GameObject WeaponBullet;
    public float damage;
    public float Attackspeed;
    public float BulletNum;

    public Sprite weaponImage;


    public float snappiness;
    public float returnSpeed;
    public float chargeAmount;
    public bool canCharge;
    public float coolTime;
    

    public float BaseRecoilX;
    public float maxChargeBonus;
    public float YZRecoil;

    public Sprite WeaponImage;

    public List<ScriptableObject> effects;
    public UpgradeCost upgradeCosts;
    public UpgradeResults upgradeResults;
    /*Unity에서 인터페이스는 Inspector에 직접 안 보임
    -> 드래그 & 드롭 안 됨,에셋 연결 안 됨
    -> 그래서 우회로로:ScriptableObject로 타입 지정
     */


}
[CreateAssetMenu(menuName = "Data/WeaponPrafabTable")]
public class WeaponPrefabTable : ScriptableObject

{
    public List<WeaponPrefabData> weaponPrafabTableDatas =
        new List<WeaponPrefabData>();
}
