using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
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
    #region UI Elements
    [HideInInspector] public GameObject attackGaugeBar;
    [HideInInspector] public Image attackGaugeBarFill;
    [HideInInspector] public Image weaponImage;
    [HideInInspector] public TMP_Text weaponText;
    [HideInInspector] public TMP_Text bulletNumText;
    #endregion

    #region Weapon & Attack Settings
    [Header("Weapon & Attack Settings")]
    public int currentWeaponNum;
    public int totalWeaponCount;

    [HideInInspector] public WeaponPrefabData currentWeaponData;
    [SerializeField] private Transform firePos;

    private List<WeaponPrefabData> _weaponList = new();
    private Dictionary<WeaponState, int> _currentAmmo = new();
    private bool _canAttack = true;
    private float _attackRatio;
    #endregion

    #region Charge & Recoil Settings
    [Header("Charge & Recoil Settings")]
    public float currentCharge = 0f;
    public float maxCharge;
    public float baseRecoilX;
    public float maxChargeBonus;

    private bool _isCharging;
    #endregion

    #region Player Components
    private CameraMovement _cameraMovement;
    private Camera _cam;
    private Rigidbody _rb;
    private Animator _anim;
    private PlayerStat _stat;
    [SerializeField]private AudioSource _audioSource;
    #endregion

    public bool CanAttack;
    
    void Start()
    {
        _currentAmmo = new()
        {
            { WeaponState.Base, GetAmmoValue(WeaponState.Base) },
            { WeaponState.Improved, GetAmmoValue(WeaponState.Improved) },
            { WeaponState.Slime, GetAmmoValue(WeaponState.Slime) },
            { WeaponState.Fire, GetAmmoValue(WeaponState.Fire) },
            { WeaponState.Toxic, GetAmmoValue(WeaponState.Toxic) },
            { WeaponState.Energy, GetAmmoValue(WeaponState.Energy) },
            { WeaponState.Bomb, GetAmmoValue(WeaponState.Bomb) }
        };

        _stat = GetComponent<PlayerStat>();
        _anim = GetComponentInChildren<Animator>();
        _cam = Camera.main;
        _cameraMovement = GetComponentInChildren<CameraMovement>();
        _rb = GetComponent<Rigidbody>();

        if (_rb != null)
        {
            _rb.freezeRotation = true;
        }

        if (GM.GetPrefabManager()?.WeaponPrefabTable?.weaponPrafabTableDatas != null)
        {
            foreach (var weapon in GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas)
            {
                _weaponList.Add(weapon);
            }
        }

        SelectWeapon(0);
        _canAttack = true;

        if (attackGaugeBarFill != null)
        {
            attackGaugeBarFill.fillAmount = 0f;
        }
    }

    private void Update()
    {
        if (CanAttack == false)
            return;
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll > 0f)
        {
            currentWeaponNum++;
            if (currentWeaponNum > totalWeaponCount - 1)
                currentWeaponNum = 0;
            SelectWeapon(currentWeaponNum);
        }
        else if (scroll < 0f)
        {
            currentWeaponNum--;
            if (currentWeaponNum < 0)
                currentWeaponNum = totalWeaponCount - 1;
            SelectWeapon(currentWeaponNum);
        }
        if (_isCharging)
        {
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp
                (currentCharge, 0f, maxCharge);
            _attackRatio = (maxCharge > 0f) ? 
                (currentCharge / maxCharge) : 0f;
            if (attackGaugeBarFill != null)
                attackGaugeBarFill.fillAmount = _attackRatio;
        }
    }
    public void SelectWeapon(int index)
    {
        if (!CanAttack || _weaponList == null || _weaponList.Count <= index || 
            !SaveManager.CurrentData.weaponActive[_weaponList[index].weaponState]) return;

        currentWeaponData = _weaponList[index];

        weaponImage.sprite = currentWeaponData.WeaponImage;
        weaponText.text = currentWeaponData.weaponState.ToString();
        bulletNumText.text = _currentAmmo[currentWeaponData.weaponState].ToString();
        _audioSource.clip = currentWeaponData.shootSound;

        if (currentWeaponData.WeaponBullet == null) return;

        maxCharge = currentWeaponData.chargeAmount;
        baseRecoilX = currentWeaponData.BaseRecoilX;
        maxChargeBonus = currentWeaponData.maxChargeBonus;

        if (attackGaugeBar != null)
            attackGaugeBar.SetActive(currentWeaponData.canCharge);
    }
    public void GetBullet(WeaponState weaponState, int bulletNum)
    {
        Debug.Log(_currentAmmo[weaponState]);
        _currentAmmo[weaponState] += bulletNum;
        if (currentWeaponData.weaponState == weaponState)
        {
            bulletNumText.text = _currentAmmo[weaponState].ToString();
        }
        Debug.Log(_currentAmmo[weaponState]);
    }
    public void ResetCharge()
    {
        _isCharging = false;
        currentCharge = 0f;
        _attackRatio = 0f;
        if (attackGaugeBarFill != null)
        {
            attackGaugeBarFill.fillAmount = 0f;
        }
    }
    void OnAttack(InputValue value)
    {
        if (!CanAttack || _currentAmmo[currentWeaponData.weaponState] <= 0) return;

        if (currentWeaponData.canCharge)
        {
            bool isPressed = value.isPressed;
            if (isPressed)
                _isCharging = true;
            else
            {
                _isCharging = false;
                Fire();
            }
            return;
        }
        Fire(); 
    }
    public void Fire()
    {
        if (!_canAttack) return;

        StartCoroutine(CoolTimeRoutine());

        _anim.SetTrigger("Attack");

        GameObject cbCopy = GM.GetPoolManager().GetBullet(currentWeaponData.weaponState, 
            firePos.position, new Vector3(0.5f, 0.5f, 0.5f),
            firePos.localRotation);
        if (cbCopy == null) return;
        CannonBall currentBall = cbCopy.GetComponent<CannonBall>();
        float finalDamage = (currentWeaponData.damage * 
            _stat.DamageMultiplier) + (_attackRatio * maxChargeBonus);
        currentBall.SetDatas(finalDamage, currentWeaponData, firePos.position, currentCharge);
        float currentRecoilX = baseRecoilX * (1f + (_attackRatio * maxChargeBonus));
        float yzRecoil = currentWeaponData.YZRecoil;
        _cameraMovement.FireRecoil(currentRecoilX, yzRecoil, yzRecoil);

        Rigidbody cannonBallRB = cbCopy.GetComponent<Rigidbody>();
        if (cannonBallRB != null)
        {
            cannonBallRB.linearVelocity = Vector3.zero; 
            cannonBallRB.angularVelocity = Vector3.zero;
            Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit, 1000f) ? 
                hit.point : ray.origin + ray.direction * 1000f;
            Vector3 shootDir = (targetPoint - firePos.position).normalized;
            cannonBallRB.AddForce(-shootDir * (currentWeaponData.Attackspeed * 
                _stat.AttackSpeedMultiplier), ForceMode.Impulse);
        }
        ResetCharge();
        _currentAmmo[currentWeaponData.weaponState]--;
        if (bulletNumText != null) bulletNumText.text = 
                _currentAmmo[currentWeaponData.weaponState].ToString();
        _audioSource.Play();
    }
    IEnumerator CoolTimeRoutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(currentWeaponData.coolTime);
        _canAttack = true;
    }
    public void HalfRemove()
    {
        Debug.Log("HalfRemove ½ÇÇà");
        EnemyMovement[] enemies = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            int ran = UnityEngine.Random.Range(0, 260);
            if (ran % 2 == 0)
            {
                enemy.GetComponent<Health>().TakeDamage(2600);
            }
        }

    }

    private int GetAmmoValue(WeaponState weaponState)
    {
        if (GM.GetPrefabManager()?.WeaponPrefabTable?.weaponPrafabTableDatas == null) return 0;

        var data = GM.GetPrefabManager().WeaponPrefabTable.weaponPrafabTableDatas.Find(x => x.weaponState == weaponState);
        if (data == null) return 0;

        int currentLevel = 0;
        if (SaveManager.CurrentData?.weaponlevel != null && SaveManager.CurrentData.weaponlevel.ContainsKey(weaponState))
        {
            currentLevel = SaveManager.CurrentData.weaponlevel[weaponState];
        }

        return (int)data.BulletNum + (int)Mathf.Pow(currentLevel, 1.15f) - 1;
    }
}

