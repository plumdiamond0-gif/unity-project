using UnityEngine;

public class gamemanager : MonoBehaviour
{
    public static gamemanager instance;
    public WeaponPrafabTable WeaponPrafabTable;
    public PlayerMovement PlayerMovement;
    public PlayerAttack playerAttack;
    public UIManager UIManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
        void Awake()
        {
            // 싱글톤 초기화 로직 (필수!)
            if (instance == null)
            {
                instance = this;
                // 씬이 바뀌어도 파괴되지 않게 하려면 추가
                // DontDestroyOnLoad(gameObject); 
            }
            else
            {
                // 이미 인스턴스가 있다면 새로 만들어진 녀석은 파괴
                Destroy(gameObject);
            }
        }
    

    public GameObject GetPrefab(string prefabname, Vector3 spawnpos, Quaternion spawnrot)
    {
        var data = WeaponPrafabTable.weaponPrafabTableDatas.Find(x => x.WeaponName == prefabname);
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



    // Update is called once per frame
    void Update()
    {
        
    }
}
