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

public class PlayerMovement : MonoBehaviour
{
    private float MovementX;
    private float MovementY;
    public float yRotation;
    public float xRotation;
    private float mouseX;
    private float mouseY;
    public float MouseSpeed;

    //[SerializeField] AudioClip walk;


    [SerializeField]private float PlayerSpeed = 1;
    [SerializeField]float walkSpeed;
    [SerializeField]float RunSpeed;
    //public float JumpPower;

    private bool isGrounded;
    bool isSprint;
    Animator anim;

    float currentSpeed;
    Coroutine walkRoutine;

    Camera cam;
    Rigidbody Rb;
    public PlayerAttack PlayerAttack;

    public bool CanMove;
    bool isWalkingSoundPlaying = false;

    [SerializeField]AudioSource walkAudio;
    [SerializeField] AudioSource _walkAudio;

    public enum PlayerState
    {
        Idle,
        InBase,
        InBattle
    }

    public PlayerState state;

    PlayerStat stat;

    void Start()
    {
        stat = GetComponent<PlayerStat>();
        //TODO:Aim = StartWeapon.transform.Find("Aim");
        stat = GetComponent<PlayerStat>();
        anim = GetComponentInChildren<Animator>();

        isSprint = false;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;
        walkAudio = GetComponent<AudioSource>();
        walkAudio.Stop();

        PlayerSpeed = walkSpeed * stat.PlayerSpeed;
        Debug.Log(PlayerSpeed);

    }

    void Update()
    {
        if (!CanMove)
            return;
        Rotate();

        float inputMagnitude = new Vector2(MovementX, MovementY).magnitude;
        float targetSpeed = 0f;

        if (state == PlayerState.InBase)
        {
            anim.SetBool("InBase", true);
            if (inputMagnitude > 0)
            {
                targetSpeed = 1;
            }
        }
        else
        {
            if (inputMagnitude > 0)
            {
                targetSpeed = isSprint ? 1f : 0.5f;

            }
        }

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 26f * Time.deltaTime);

        anim.SetFloat("Speed", currentSpeed);

        anim.SetFloat("MoveX", MovementX);
        anim.SetFloat("MoveY", MovementY);
        anim.SetBool("IsGrounded", isGrounded);


        bool isMoving = MovementX != 0 || MovementY != 0;

        if (isMoving && !isWalkingSoundPlaying && isGrounded)
        {
            if (isSprint)
                walkAudio.Play();
            else
                if (walkRoutine == null)
            {
                walkRoutine = StartCoroutine(walk());
            }

            isWalkingSoundPlaying = true;
        }
        else if (!isMoving && isWalkingSoundPlaying || !isGrounded)
        {
            walkAudio.Stop();
            if(walkRoutine != null)
            StopCoroutine(walkRoutine);
            walkRoutine = null;
            isWalkingSoundPlaying = false;
        }

    }
    IEnumerator walk()
    {
        while(!isSprint)
        {
            _walkAudio.Play();
            yield return new WaitForSeconds(0.8f); 
        }
        walkRoutine = null;
    }

    void OnMove(InputValue inputValue)
        {
        if(!CanMove)
            return;
        //GM.GetSoundManager().PlaySFX(walk);
            Vector2 Movevalue = inputValue.Get<Vector2>();
            MovementX = Movevalue.x;
            MovementY = Movevalue.y;

        }
    void OnJump(InputValue inputValue)
        {
            if (!isGrounded)
            {
                return;
            }
        anim.SetTrigger("Jump");

        Rb.linearVelocity = new Vector3(Rb.linearVelocity.x, 0, Rb.linearVelocity.z);
            Rb.AddForce(transform.up * stat.JumpPower, ForceMode.Impulse);
            isGrounded = false;

    }
    void OnSprint()
        {
        if(state == PlayerState.InBase) 
            return;

        Debug.Log("Sprint");
        isSprint = !isSprint;

        PlayerSpeed = isSprint ? RunSpeed*stat.PlayerSpeed : walkSpeed*stat.PlayerSpeed;
        
    }

  


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("¾ÏÀÎ±×¶ó¿îµå");
            isGrounded = true;

        }
    }


    private void FixedUpdate()
    {
        if (!CanMove)
            return;
        Vector3 MoveDir = ((transform.right * MovementX) + (transform.forward * MovementY)).normalized;
        Vector3 targetVel = MoveDir * PlayerSpeed;

        Vector3 currentVel = Rb.linearVelocity;

        Vector3 newVel = Vector3.Lerp(
            new Vector3(currentVel.x, 0, currentVel.z),
            targetVel,
            10f * Time.deltaTime
        );

        Rb.linearVelocity = new Vector3(
            newVel.x,
            currentVel.y, 
            newVel.z
        );
        // transform.position = new Vector3(MovementX, transform.position.y, MovementY); 


    }

    //public void RestartGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    void Rotate()
    {
     
        Vector2 mouseVec = Mouse.current.delta.ReadValue();
        mouseX = mouseVec.x * MouseSpeed * Time.deltaTime;
        mouseY = mouseVec.y * MouseSpeed * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -80f, 45f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        //if (PlayerAttack.spawnedWeapon != null)
        //{
        //    Debug.Log("¿ý");
        //    PlayerAttack.spawnedWeapon.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //}
     
    }


}
/* 1ÀÎÄª

 * void Rotate()
 {
     Vector2 mouseVec = Mouse.current.delta.ReadValue();
     float mouseX = mouseVec.x * MouseSpeed * Time.deltaTime;
     float mouseY = mouseVec.y * MouseSpeed * Time.deltaTime;
     xRotation -= mouseY;
     yRotation += mouseX;

     xRotation = Mathf.Clamp(xRotation, -90f, 90f);

     cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
     transform.rotation = Quaternion.Euler(0, yRotation, 0);

 }*/
