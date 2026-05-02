using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "Data/WeaponPrafabTable")]
public class WeaponPrefabTable : ScriptableObject

{
    [System.Serializable]
    public class WeaponPrefabTableData
    {
        public enum WeaponState
        {
            Pistol,
            Cannon,
            PulseGun,
            MissileLauncher,
            SlimeGun,
        }
        public WeaponState weaponState;
        public string WeaponName;
        public GameObject Weapon;
        public GameObject WeaponBullet;
        public float damage;
        public float Attackspeed;
        public float snappiness;
        public float returnSpeed;
        public float chargeAmount;
        public bool canCharge;
        public float coolTime;
        public float BaseRecoilX;
        public List<ScriptableObject> effects;
        /*Unity에서 인터페이스는 Inspector에 직접 안 보임
        -> 드래그 & 드롭 안 됨,에셋 연결 안 됨
        -> 그래서 우회로로:ScriptableObject로 타입 지정
         */


    }
    public List<WeaponPrefabTableData> weaponPrafabTableDatas =
        new List<WeaponPrefabTableData>();
}
