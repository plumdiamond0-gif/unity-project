using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
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
    private float addDamage;
    Animator anim;
     List<WeaponPrefabTableData> weaponList = new();



    public KnockBackEffect knock;

    public GameObject AttackGuageBar;

    public Image AttackGuageBarFill;


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

        SelectWeapon(0);
        canAttack = true;
        addDamage = 0;
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
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
            AttackRatio = currentCharge / maxCharge;
            AttackGuageBarFill.fillAmount = AttackRatio;
            
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
        maxChargeBonus = currentweapondata.maxChargeBonus;
        Aim = aimTrans;
        if (currentweapondata.canCharge)
            {
                AttackGuageBar.SetActive(true);
            }
            else
            {
                AttackGuageBar.SetActive(false);
            }
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
                knock.GetKnockVal(currentCharge, currentweapondata);

                Fire();
           
               
            }

            return;
        }

        Fire(); // 일반 무기
    }

    //void ReadyAttack()
    //{
    //    float finalDamage;
    //    finalDamage = currentweapondata.damage + (addDamage + AttackRatio * 10);
    //    CannonBall currentBall = currentweapondata.WeaponBullet.GetComponent<CannonBall>();
    //    currentBall.SetWeaponData(currentweapondata);
    //    currentBall.SetDamage(finalDamage);
    //    Debug.Log(finalDamage);
    //    Fire();
    //    anim.SetTrigger("Attack");
    //}

    public void RestCharge()
    {
        isCharging = false;
        currentCharge = 0;
        AttackRatio = 0;
        AttackGuageBarFill.fillAmount = 0;
        
    }

    public void Fire()
    {
       

        if (!canAttack)
        {
            return;
        }
        StartCoroutine(coolTimeRouctine());
        anim.SetTrigger("Attack");

        GameObject CBcopy = GameManager.instance.GetPrefab
            (currentweapondata.WeaponName, Aim.transform.position, Quaternion.identity);

        float finalDamage;
        CannonBall currentBall = CBcopy.GetComponent<CannonBall>();

        finalDamage = currentweapondata.damage + (addDamage + AttackRatio * 10);
        currentBall.SetWeaponData(currentweapondata);
        currentBall.SetDamage(finalDamage);
        Debug.Log(finalDamage);

        float currnetRecoilX = baseRecoilX * (1f + (AttackRatio * maxChargeBonus));
        float YZRecoil = currentweapondata.YZRecoil;
        cameraMovement.FireRecoil(currnetRecoilX, YZRecoil, YZRecoil);

        RestCharge();
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



