using System.Collections;
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

    bool canAttack;
   // [SerializeField] private Transform WeaponSpawnPos;
    private bool isCharging;
    private float AttackRatio;
    CameraMovement cameraMovement;
    Transform Aim;
    Camera cam;
    Rigidbody Rb;
    public GameObject spawnedWeapon;
    public WeaponPrefabTableData currentweapondata;
    [SerializeField] private Transform Firepos;
    private float addDamage = 0;
    Animator anim;
    public List<WeaponPrefabTableData> weaponList = new();


    //public Dictionary<int, WeaponState> weaponDic = new();
    void Start()
    {
        foreach (var weapon in GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas)
        {
            weaponList.Add(weapon);
        }
        anim = GetComponentInChildren<Animator>();    
        //weaponList = GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas;
       
        cam = Camera.main;
        cameraMovement = cam.GetComponentInChildren<CameraMovement>();
        //WeaponSpawnPos = transform.Find("WeaponSpawnPos");
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;

        currentweapondata = weaponList[0];
        GameObject startweapon = Instantiate(currentweapondata.Weapon, Firepos.position, Firepos.rotation, Firepos);
        spawnedWeapon = startweapon;
        maxCharge = currentweapondata.chargeAmount;
        Aim = spawnedWeapon.transform.Find("Aim");
        canAttack = true;
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
       spawnedWeapon = Instantiate(currentweapondata.Weapon, Firepos.position, Firepos.rotation, Firepos);
        Transform aimTrans = spawnedWeapon.transform.Find("Aim");
        maxCharge = currentweapondata.chargeAmount;
        baseRecoilX = currentweapondata.BaseRecoilX;
        Aim = aimTrans;
    }


    void OnAttack(InputValue value)
    {
        if (currentweapondata.canCharge)
        {
            bool isPressed = value.isPressed;

            if (isPressed)
            {
                isCharging = true;
            }
            else
            {
                isCharging = false;
                ReadyAttack();
                RestCharge();
            }

            return;
        }

        ReadyAttack(); // 일반 무기
    }

    void ReadyAttack()
    {
        float finalDamage;
        finalDamage = currentweapondata.damage + (addDamage + AttackRatio * 10);
        CannonBall currentBall = currentweapondata.WeaponBullet.GetComponent<CannonBall>();
        currentBall.SetDamage(finalDamage);
        Debug.Log(finalDamage);
        Fire();
        anim.SetTrigger("Attack");
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
       

        if (!canAttack)
        {
            return;
        }
        StartCoroutine(coolTimeRouctine());

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

    IEnumerator coolTimeRouctine()
    {
        canAttack = false;
        yield return new WaitForSeconds(currentweapondata.coolTime);
        canAttack = true;
    }

    public void DamageUpdate(float val)
    {
        addDamage += val;

    }
}



