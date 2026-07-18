

using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static PlayerAttack;
using static WeaponPrefabTable;
public enum PlayerState
{
    Idle,
    InBase,
    InBattle
}
public class PlayerMovement : MonoBehaviour
{
    #region Rotation
    private float _yRotation;
    public float xRotation;
    #endregion

    #region Mouse
    private float _mouseX;
    private float _mouseY;
    [SerializeField] private float _mouseSpeed = 26f; // 초기값을 주지 않으면 마우스 회전이 안 됩니다.
    #endregion

    #region Movement
    private float _movementX;
    private float _movementY;
    [Header("Movement")]
    [SerializeField] private float playerSpeed = 1f;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpPower;


    private float _currentSpeed;
    public bool canMove;
    #endregion

    #region State
    private bool _isGrounded;
    private bool _isSprinting;

    public PlayerState state;


    #endregion

    #region Components
    private Animator _anim;
    private Rigidbody _rb;
    private Camera _cam;
    private PlayerStat _stat;

    [SerializeField] private AudioSource _walkAudio;
    public PlayerAttack playerAttack;
    #endregion

    #region Coroutines
    private Coroutine _walkRoutine;
    #endregion

    #region Audio
    private bool _isWalkingSoundPlaying;
    #endregion

    void Start()
    {
        _stat = GetComponent<PlayerStat>();
        _anim = GetComponentInChildren<Animator>();

        _isSprinting = false;
        _cam = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        if (_walkAudio == null) _walkAudio = GetComponent<AudioSource>();
        _walkAudio.Stop();

        //주의: _stat이 null일 경우를 대비해 인스펙터 확인이 필요합니다.
        if (_stat != null)
        {
            playerSpeed = walkSpeed * _stat.MoveSpeedMultiplier;
        }
    }

    void Update()
    {


        if (!canMove)
            return;

        Rotate();

        float inputMagnitude = new Vector2(_movementX, _movementY).magnitude;
        float targetSpeed = 0f;

        if (state == PlayerState.InBase)
        {
            _anim.SetBool("InBase", true);
            if (inputMagnitude > 0)
            {
                targetSpeed = 1f;
            }
        }
        else if(state == PlayerState.InBattle)
        {
            if (inputMagnitude > 0)
            {
                targetSpeed = _isSprinting ? 1f : 0.5f;
            }
        }

        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, 26f * Time.deltaTime);

        _anim.SetFloat("Speed", _currentSpeed);
        _anim.SetFloat("MoveX", _movementX);
        _anim.SetFloat("MoveY", _movementY);
        _anim.SetBool("IsGrounded", _isGrounded);

        bool isMoving = _movementX != 0 || _movementY != 0;

        if (isMoving && !_isWalkingSoundPlaying && _isGrounded)
        {
            if (_isSprinting)
            {
                Debug.Log("Sprinting");
                _walkAudio.Play();
            }
            else
            {
                if (_walkRoutine == null)
                {
                    _walkRoutine = StartCoroutine(walk());
                }
            }

            _isWalkingSoundPlaying = true;
        }
        else if ((!isMoving && _isWalkingSoundPlaying) || !_isGrounded)
        {
            _walkAudio.Stop();
            if (_walkRoutine != null)
            {
                StopCoroutine(_walkRoutine);
            }
            _walkRoutine = null;
            _isWalkingSoundPlaying = false;
        }
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        Vector3 MoveDir = ((transform.right * _movementX) + (transform.forward * _movementY)).normalized;

        Vector3 targetVel = MoveDir * playerSpeed * _stat.MoveSpeedMultiplier;

        Vector3 currentVel = _rb.linearVelocity;

        Vector3 newVel = Vector3.Lerp(
            new Vector3(currentVel.x, 0, currentVel.z),
            targetVel,
            10f * Time.deltaTime
        );

        _rb.linearVelocity = new Vector3(
            newVel.x,
            currentVel.y,
            newVel.z
        );
    }

    IEnumerator walk()
    {
        while (!_isSprinting)
        {
            _walkAudio.Play();
            yield return new WaitForSeconds(0.8f);
        }
        _walkRoutine = null;
    }

    void OnMove(InputValue inputValue)
    {
        if (!canMove)
            return;

        Vector2 Movevalue = inputValue.Get<Vector2>();
        _movementX = Movevalue.x;
        _movementY = Movevalue.y;
    }

    void OnJump(InputValue inputValue)
    {
        if (!canMove || !_isGrounded)
            return;

        _anim.SetTrigger("Jump");

        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);

        _rb.AddForce(transform.up * jumpPower * _stat.JumpPowerMultiplier, ForceMode.Impulse);
        _isGrounded = false;
    }

    void OnSprint()
    {
        if (!canMove || state == PlayerState.InBase)
            return;

        _isSprinting = !_isSprinting;

        playerSpeed = _isSprinting ? runSpeed : walkSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!canMove)
            return;

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("암인그라운드");
            _isGrounded = true;
        }
    }

    void Rotate()
    {
        Vector2 mouseVec = Mouse.current.delta.ReadValue();
        _mouseX = mouseVec.x * _mouseSpeed * Time.deltaTime;
        _mouseY = mouseVec.y * _mouseSpeed * Time.deltaTime;

        xRotation -= _mouseY;
        _yRotation += _mouseX;
        transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }

    public void RecallBeacon(float val)
    {
        StartCoroutine(RecallRoutine(val, transform.position));
    }

    IEnumerator RecallRoutine(float val, Vector3 pos)
    {
        yield return new WaitForSeconds(val);
        transform.position = pos;
    }
}

