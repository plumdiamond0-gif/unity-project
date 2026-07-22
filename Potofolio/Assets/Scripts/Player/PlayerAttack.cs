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
        HandleWeaponScroll();
        HandleCharging();
    }

    private void HandleWeaponScroll()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll > 0f)
        {
            currentWeaponNum++;
            if (currentWeaponNum > totalWeaponCount - 1)
            {
                currentWeaponNum = 0;
            }
            SelectWeapon(currentWeaponNum);
        }
        else if (scroll < 0f)
        {
            currentWeaponNum--;
            if (currentWeaponNum < 0)
            {
                currentWeaponNum = totalWeaponCount - 1;
            }
            SelectWeapon(currentWeaponNum);
        }
    }

    private void HandleCharging()
    {
        if (_isCharging)
        {
            currentCharge += Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxCharge);
            _attackRatio = (maxCharge > 0f) ? (currentCharge / maxCharge) : 0f;

            if (attackGaugeBarFill != null)
            {
                attackGaugeBarFill.fillAmount = _attackRatio;
            }
        }
    }

    public void SelectWeapon(int index)
    {
        if (!CanAttack || _weaponList == null || _weaponList.Count <= index || !SaveManager.CurrentData.weaponActive[_weaponList[index].weaponState]) return;

        currentWeaponData = _weaponList[index];

        weaponImage.sprite = currentWeaponData.weaponImage;
        weaponText.text = currentWeaponData.weaponState.ToString();
        bulletNumText.text = _currentAmmo[currentWeaponData.weaponState].ToString();
        _audioSource.clip = currentWeaponData.shootSound;

        if (currentWeaponData.WeaponBullet == null) return;

        maxCharge = currentWeaponData.chargeAmount;
        baseRecoilX = currentWeaponData.BaseRecoilX;
        maxChargeBonus = currentWeaponData.maxChargeBonus;

        if (attackGaugeBar != null)
        {
            attackGaugeBar.SetActive(currentWeaponData.canCharge);
        }
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
            {
                _isCharging = true;
            }
            else
            {
                _isCharging = false;

                if (currentWeaponData.effects != null)
                {
                    foreach (var effect in currentWeaponData.effects)
                    {
                        if (effect is DotdamEffect dotdamEffect)
                        {
                            dotdamEffect.GetCharge(currentCharge);
                        }
                        if (effect is KnockBackEffect knockBackEffect)
                        {
                            knockBackEffect.GetChrage(currentCharge);
                        }
                        if (effect is RangeEffect rangeEffect)
                        {
                            rangeEffect.GetCharge(currentCharge);
                        }
                    }
                }
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

        if (_anim != null)
        {
            _anim.SetTrigger("Attack");
        }

        GameObject cbCopy = GM.GetBulletManager().GetBullet(currentWeaponData.weaponState, firePos.position,
            firePos.localRotation);

        if (cbCopy == null) return;
        CannonBall currentBall = cbCopy.GetComponent<CannonBall>();
        float finalDamage = (currentWeaponData.damage * _stat.DamageMultiplier) + (_attackRatio * maxChargeBonus);

        if (currentBall != null)
        {
            currentBall.SetWeaponData(currentWeaponData);
            currentBall.SetDamage(finalDamage);
            currentBall.SetPos(firePos.position);

        }

        float currentRecoilX = baseRecoilX * (1f + (_attackRatio * maxChargeBonus));
        float yzRecoil = currentWeaponData.YZRecoil;
        if (_cameraMovement != null)
        {
            _cameraMovement.FireRecoil(currentRecoilX, yzRecoil, yzRecoil);
        }

        ResetCharge();

        Rigidbody cannonBallRB = cbCopy.GetComponent<Rigidbody>();
        if (cannonBallRB != null)
        {
            if (_cam == null) Debug.LogWarning("메인 카메라를 찾을 수 없습니다!");
            cannonBallRB.linearVelocity = Vector3.zero; // Unity 6
            cannonBallRB.angularVelocity = Vector3.zero;
            Ray ray = _cam != null ? _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f)) : new Ray(firePos.position, firePos.forward);
            Vector3 targetPoint = Physics.Raycast(ray, out RaycastHit hit, 1000f) ? hit.point : ray.origin + ray.direction * 1000f;

            Vector3 shootDir = (targetPoint - firePos.position).normalized;
            Vector3 rightAxis = Vector3.Cross(Vector3.up, shootDir).normalized;
            shootDir = Quaternion.AngleAxis(0f, rightAxis) * shootDir;

            float attackSpeedStat = (_stat != null) ? _stat.AttackSpeedMultiplier : 1f;

            cannonBallRB.AddForce(-shootDir * (currentWeaponData.Attackspeed * attackSpeedStat), ForceMode.Impulse);
        }
        _currentAmmo[currentWeaponData.weaponState]--;
        if (bulletNumText != null)
        {
            bulletNumText.text = _currentAmmo[currentWeaponData.weaponState].ToString();
        }
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
        Debug.Log("HalfRemove 실행");
        EnemyMovement[] enemies = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None);
        int enemyNum = 0;
        foreach (var enemy in enemies)
        {
            int ran = UnityEngine.Random.Range(0, 260);
            if (ran % 2 == 0)
            {
                enemyNum++;
                Destroy(enemy.gameObject); 
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

