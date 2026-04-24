using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance;
   [SerializeField] private PrefabManager prefabManager;
    public PlayerMovement PlayerMovement;
    public PlayerAttack playerAttack;
    public UIManager UIManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 

    public PrefabManager GetPrefabManager()
    {
        return prefabManager;
    }
        void Awake()
        {
            // ½Ì±ÛÅæ ÃÊ±âÈ­ ·ÎÁ÷ (ÇÊ¼ö!)
            if (instance != null && instance != this)
            {

            }
        instance = this;
        DontDestroyOnLoad(gameObject);

        {
            prefabManager = PrefabManager.CreatePrefabManager(prefabManager.gameObject, transform);
        }
        }


    public GameObject GetPrefab(string prefabname, Vector3 spawnpos, Quaternion spawnrot)
    {
        var data = prefabManager.WeaponPrefabTable.weaponPrafabTableDatas.Find(x => x.WeaponName == prefabname);
        if (data != null)
        {
            GameObject CBcoy = Instantiate(data.WeaponBullet, spawnpos, spawnrot);
            return CBcoy;
        }
        else
        {
            return null;
        }
    }



}
public static class GM
{
    public static PrefabManager GetPrefabManager()
    {
        return gamemanager.instance.GetPrefabManager();
    }
}

