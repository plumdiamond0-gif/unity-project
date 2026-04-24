using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static WeaponPrefabTable;

public class PlayerAttack : MonoBehaviour
{

    [Header("무기,공격")]
    public int currntWeaponNum;
    public int WeaponNum;
    public float CanonBallspeed;

    [Header("차지,반동")]
    public float currentCharge = 0f;
    public float maxCharge;
    public float baseRecoilX;
    public float maxChargeBonus;

    [Header("수동")]
    public List<WeaponPrefabTableData> weaponList = new();

    private Transform WeaponSpawnPos;
    private bool isCharging;
    private float AttackRatio;
    CameraMovement cameraMovement;
    private Transform Aim;
    Camera cam;
    Rigidbody Rb;
    public GameObject spawnedWeapon;
    public WeaponPrefabTableData currentweapondata;




    //public Dictionary<int, WeaponState> weaponDic = new();
    void Start()
    {
        weaponList = GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas;
        cam = Camera.main;
        cameraMovement = cam.GetComponentInChildren<CameraMovement>();
        WeaponSpawnPos = transform.Find("WeaponSpawnPos");
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;

        currentweapondata = weaponList[0];
        GameObject startweapon = Instantiate(currentweapondata.Weapon, WeaponSpawnPos.position, WeaponSpawnPos.rotation, transform);
        spawnedWeapon = startweapon;
        Aim = spawnedWeapon.transform.Find("Aim");
    }

    private void Update()
    {
        float Scrool = Mouse.current.scroll.ReadValue().y;
        if (Scrool > 0)
        {
            currntWeaponNum++;
            if (currntWeaponNum > WeaponNum - 1)
            {
                currntWeaponNum = 0;
            }
            SelectWeapon(currntWeaponNum);
        }
        else if (Scrool < 0)
        {
            currntWeaponNum--;
            if (currntWeaponNum < 0)
            {
                currntWeaponNum = WeaponNum - 1;
                //List는 0부터 시작
            }
            SelectWeapon(currntWeaponNum);

        }

        if(isCharging)
        {
            Debug.Log("IsCharging");
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
            AttackRatio = currentCharge / maxCharge;
            if (gamemanager.instance.UIManager != null)
            {
                gamemanager.instance.UIManager.AttackGuageBarFill.fillAmount = AttackRatio;
            }
        }
        
    }

    public void SelectWeapon(int index)
    {
        
        if (spawnedWeapon != null)
        {
            Destroy(spawnedWeapon);
        }
        currentweapondata = weaponList[index];
        if (currentweapondata.Weapon == null)
        {
            Debug.LogError($"{currentweapondata.WeaponName}의 프리팹 원본이 이미 파괴되었거나 할당되지 않았습니다!");
            return;
        }
        Debug.Log("CurrntWeapon : " + currentweapondata.WeaponName);
       spawnedWeapon = Instantiate(currentweapondata.Weapon, WeaponSpawnPos.position, WeaponSpawnPos.rotation, gameObject.transform);
        Transform aimTrans = spawnedWeapon.transform.Find("Aim");
        Aim = aimTrans;
    }

    
    void OnAttack(InputValue value)
    {
        bool ispressed = value.isPressed;

        if (currentweapondata.canCharge)
        {
            isCharging = ispressed;
            if(!ispressed)
            {
                //Debug.Log("비율 적용 이전 데미지 : " + currentweapondata.damage);
                //Debug.Log("비율 : " + AttackRatio);
                currentweapondata.damage += AttackRatio*10;
                //Debug.Log("비율 적용 이후 데미지 : " + currentweapondata.damage);
                Fire();
                currentweapondata.damage -= AttackRatio * 10;

                RestCharge();
            }
        }
        else
        {
            if(ispressed)
            Fire();
        }


        //if()


    }

    public void RestCharge()
    {
        isCharging = false;
        currentCharge = 0;
        if (gamemanager.instance.UIManager != null)
        {
            gamemanager.instance.UIManager.AttackGuageBarFill.fillAmount = 0;
        }
    }

    public void Fire()
    {
        Debug.Log("발사;");
        float currnetRecoilX = baseRecoilX * (1f + (AttackRatio * maxChargeBonus));
        cameraMovement.FireRecoil(currnetRecoilX, 0.5f, 0.5f);

        GameObject CBcopy = gamemanager.instance.GetPrefab
            (currentweapondata.WeaponName, Aim.transform.position, Quaternion.identity);
        CannonBall cannonBall = CBcopy.GetComponent<CannonBall>();
        if (cannonBall != null)
        {
            cannonBall.SetWeaponData(currentweapondata);
        }

        if (CBcopy == null)
        {
            Debug.Log("캐논볼 없음");
            return;

        }
        Rigidbody CanonBallRB = CBcopy.GetComponent<Rigidbody>();
        if (CanonBallRB != null)
        {

            CanonBallRB.AddForce(Aim.transform.forward * CanonBallspeed, ForceMode.Impulse);
        }
    }

    public void Damage(float val)
    {
        currentweapondata.damage += val;
    }
}



