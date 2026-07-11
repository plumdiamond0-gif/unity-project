using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.HighDefinition.ScalableSettingLevelParameter;
using static WeaponPrefabTable;

public class PlayerAttack : MonoBehaviour
{

    [Header("무기,공격")]
    public int currntWeaponNum;
    public int WeaponNum;
    //public float CanonBallspeed;


    [Header("차지,반동")]
    public float currentCharge = 0f;
    public float maxCharge;
    public float baseRecoilX;
    public float maxChargeBonus;

    bool canAttack;
    private bool isCharging;
    private float AttackRatio;
    CameraMovement cameraMovement;
    Camera cam;
    Rigidbody Rb;
    public WeaponPrefabData currentweapondata;
    [SerializeField] private Transform Firepos;
    Animator anim;
    List<WeaponPrefabData> weaponList = new();


    public GameObject AttackGuageBar;

    public Image AttackGuageBarFill;

    public Image WeapomImage;
    public TMP_Text WeapomText;
    public TMP_Text BulletNum;

    Dictionary<WeaponState, int> currentAmmo = new();
  

    PlayerStat stat;

    void Start()
    {
        currentAmmo = new()
    {
        {WeaponState.Base, GetAmoVal(WeaponState.Base)
        },
        {WeaponState.Improved, GetAmoVal(WeaponState.Improved)
        },
                {WeaponState.Slime, GetAmoVal(WeaponState.Slime)
        },
        {WeaponState.Fire, GetAmoVal(WeaponState.Fire)
        },
                {WeaponState.Toxic, GetAmoVal(WeaponState.Toxic)
        },
        {WeaponState.Energy, GetAmoVal(WeaponState.Energy)
        },
               {WeaponState.Bomb, GetAmoVal(WeaponState.Bomb)
        },

    };

        stat  = GetComponent<PlayerStat>();
        foreach (var weapon in GM.GetPrefabManager().
            WeaponPrefabTable.weaponPrafabTableDatas)
        {
            weaponList.Add(weapon);
        }
        anim = GetComponentInChildren<Animator>();    
        //weaponList = GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas;
       
        cam = Camera.main;

        cameraMovement = GetComponentInChildren<CameraMovement>();
        //WeaponSpawnPos = transform.Find("WeaponSpawnPos");
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;

        SelectWeapon(0);
        canAttack = true;
    
        AttackGuageBarFill.fillAmount = 0;
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
  
        //if (spawnedWeapon != null)
        //{
        //    Destroy(spawnedWeapon);
        //}
        currentweapondata = weaponList[index];
        WeapomImage.sprite = currentweapondata.weaponImage;
        WeapomText.text = currentweapondata.weaponState.ToString();

        BulletNum.text = currentAmmo[currentweapondata.weaponState].ToString();

        if (currentweapondata.WeaponBullet == null)
        {
          //  Debug.LogError($"{currentweapondata.WeaponName}의 프리팹 원본이 이미 파괴되었거나 할당되지 않았습니다!");
            return;
        }
        //Debug.Log("CurrntWeapon : " + currentweapondata.WeaponName);
       //spawnedWeapon = Instantiate(currentweapondata.WeaponBullet, Firepos.position, Firepos.rotation, Firepos);
        maxCharge = currentweapondata.chargeAmount;
        baseRecoilX = currentweapondata.BaseRecoilX;
        maxChargeBonus = currentweapondata.maxChargeBonus;
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
        
        if(currentAmmo[currentweapondata.weaponState] <= 0 )
            return;

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
                foreach (var effect in currentweapondata.effects)
                {
                    if(effect is DotdamEffect dotdamEffect)
                    {
                        dotdamEffect.GetCharge(currentCharge);
                    }
                    if (effect is KnockBackEffect knockBackEffect)
                    {
                        knockBackEffect.GetCharge(currentCharge);
                    }
                }

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
            (currentweapondata.weaponState.ToString(), Firepos.transform.position, Quaternion.identity);

        float finalDamage;
        CannonBall currentBall = CBcopy.GetComponent<CannonBall>();

        finalDamage = currentweapondata.damage *stat.BaseDamage +(AttackRatio * maxChargeBonus);
        currentBall.SetWeaponData(currentweapondata);
        currentBall.SetDamage(finalDamage);
        //Debug.Log(finalDamage); 

        float currnetRecoilX = baseRecoilX * (1f + (AttackRatio * maxChargeBonus));
        float YZRecoil = currentweapondata.YZRecoil;
        cameraMovement.FireRecoil(currnetRecoilX, YZRecoil, YZRecoil);

        RestCharge();
        Rigidbody CanonBallRB = CBcopy.GetComponent<Rigidbody>();
        if (CanonBallRB != null)
        {
            if(cam == null)
                Debug.Log("nocma!!!!");
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

            Vector3 targetPoint;

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.origin + ray.direction * 1000f;
            }
            Vector3 shootDir =
     (targetPoint - Firepos.position).normalized;

            Vector3 rightAxis =
                Vector3.Cross(Vector3.up, shootDir).normalized;

            shootDir =
                Quaternion.AngleAxis(0f,rightAxis) * shootDir;

            CanonBallRB.AddForce(
                -shootDir * (currentweapondata.Attackspeed * stat.AttackSpeed),
                ForceMode.Impulse
            );
            currentAmmo[currentweapondata.weaponState]--;

            BulletNum.text = currentAmmo[currentweapondata.weaponState].ToString();


            // CanonBallRB.AddForce(Firepos.transform.forward * currentweapondata.Attackspeed, ForceMode.Impulse);
        }
    }

    IEnumerator coolTimeRouctine()
    {
        canAttack = false;
        yield return new WaitForSeconds(currentweapondata.coolTime);
        canAttack = true;
    }

    //public void DamageUpdate(float val)
    //{
    //    stat.baseDamage += val;
    //}


    public void HalfRemove()
    {
        Debug.Log("HalfRemove");
        EnemyMovement[] enemies = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);

        foreach (var enemy in enemies)
        {
            int ran = UnityEngine.Random.Range(0, 260);
            if(ran%2==0)
                Destroy(enemy);
        }
    }

    int GetAmoVal(WeaponState weaponState)
    {
        return ((int)GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas
            .Find(x => x.weaponState == weaponState).BulletNum +
            (int)Mathf.Pow(SaveManager.CurrentData.weaponlevel[weaponState], 1.15f) - 1);
    }
}



