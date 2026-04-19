using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "Data/WeaponPrafabTable")]
public class WeaponPrafabTable : ScriptableObject

{
    [System.Serializable]
    public class WeaponPrafabTableData
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
        public bool canCharge;
        public float snappiness;
        public float returnSpeed;



    }
    public List<WeaponPrafabTableData> weaponPrafabTableDatas =
        new List<WeaponPrafabTableData>();
}
