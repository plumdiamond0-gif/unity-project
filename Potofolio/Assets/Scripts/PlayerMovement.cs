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
    Animator anim;



  
    Camera cam;
    Rigidbody Rb;
    public PlayerAttack PlayerAttack;



    void Start()
    {
        //TODO:Aim = StartWeapon.transform.Find("Aim");

        anim = GetComponentInChildren<Animator>();


        isSprint = false;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Rb = GetComponent<Rigidbody>();
        Rb.freezeRotation = true;
    }

    void Update()
    {
        Rotate();

        float speed = Mathf.Clamp01(new Vector2(MovementX, MovementY).magnitude);

        anim.SetFloat("MoveX", MovementX);
        anim.SetFloat("MoveY", MovementY);
        anim.SetFloat("Speed", speed);
        anim.SetBool("IsGrounded", isGrounded);
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

            anim.SetTrigger("Jump");
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
            Debug.Log("¾ÏÀÎ±×¶ó¿îµå");
            isGrounded = true;

        }
    }


    private void FixedUpdate()
    {

        Vector3 MoveDir = ((transform.right * MovementX) + (transform.forward * MovementY)).normalized;
        Rb.linearVelocity = new Vector3(MoveDir.x * PlayerSpeed, Rb.linearVelocity.y, MoveDir.z * PlayerSpeed);


        // transform.position = new Vector3(MovementX, transform.position.y, MovementY); 


    }

    //public void RestartGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    void Rotate()
    {
        if(PlayerAttack.spawnedWeapon == null)
        {
            Debug.Log("¿ý");
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
