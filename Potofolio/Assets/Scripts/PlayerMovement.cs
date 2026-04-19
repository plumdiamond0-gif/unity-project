using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static WeaponPrafabTable;

public class PlayerMovement : MonoBehaviour
{
    private float MovementX;
    private float MovementY;
    public float yRotation;
    public float xRotation;
    private float mouseX;
    private float mouseY;

    public float PlayerSpeed;
    public float PlayerHp;
    public float JumpPower;
    public float MouseSpeed;
    public float SprintSpeed;

    private bool isGrounded;
    bool isSprint;


    public Animator anim;

  
    Camera cam;
    Rigidbody Rb;
    public PlayerAttack PlayerAttack;



    void Start()
    {
        //TODO:Aim = StartWeapon.transform.Find("Aim");



        anim = GetComponent<Animator>();
        isSprint = false;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;
    }

    private void Update()
    {

        Rotate();
    }


        void OnMove(InputValue inputValue)
        {
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
            Rb.linearVelocity = new Vector3(Rb.linearVelocity.x, 0, Rb.linearVelocity.z);
            Rb.AddForce(transform.up * JumpPower, ForceMode.Impulse);
            isGrounded = false;

        }
        void OnSprint()
        {
            if (!isSprint)
            {
                PlayerSpeed += SprintSpeed;
                isSprint = true;
            }

            else
            {
                PlayerSpeed -= SprintSpeed;
                isSprint = false;
            }
        }
    
   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("암인그라운드");
            isGrounded = true;

        }
    }


    private void FixedUpdate()
    {
        // 이동 물리 처리
        Vector3 MoveDir = ((transform.right * MovementX) + (transform.forward * MovementY)).normalized;
        Rb.linearVelocity = new Vector3(MoveDir.x * PlayerSpeed, Rb.linearVelocity.y, MoveDir.z * PlayerSpeed);


            float inputMagnitude = new Vector2(MovementX, MovementY).magnitude;

            // 콘솔창에 숫자가 0에서 1까지 잘 찍히는지 보세요!
            Debug.Log("현재 입력 크기: " + inputMagnitude);

            anim.SetFloat("MoveSpeed", inputMagnitude, 0.1f, Time.fixedDeltaTime);
        
    }

    //public void RestartGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    void Rotate()
    {
        if(PlayerAttack.spawnedWeapon == null)
        {
            Debug.Log("웡");
            return; }
        Vector2 mouseVec = Mouse.current.delta.ReadValue();
        mouseX = mouseVec.x * MouseSpeed * Time.deltaTime;
        mouseY = mouseVec.y * MouseSpeed * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        PlayerAttack.spawnedWeapon.transform.rotation = Quaternion.Euler(xRotation,yRotation, 0);
     
    }
}
/* 1인칭

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
