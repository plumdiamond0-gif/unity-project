using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/UpgradeEffect")]

public class UpgradeEffect : ScriptableObject
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [System.Serializable]
    public class UpgradeEffectData
    {
        public float DamageUp;
        public float SpeedUp;
        public float BulletNumUp;

    }
    public List<UpgradeEffectData> UpgradeEffects =
        new List<UpgradeEffectData>();
}
