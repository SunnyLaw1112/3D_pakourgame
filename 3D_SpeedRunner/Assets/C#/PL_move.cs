using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL_move : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float speed;

    public GameObject A;

    static public float BP;
    static public float MAX_BP;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplir;
    bool readyToJump;
    bool canDoubleJump;
    bool canMove;
    bool 加速道具;
    bool 道具箱;

    public float slipCooldown;
    bool readyToslip;
    bool doublejump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode slipperKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        readyToslip = true;
        doublejump = false;
        canDoubleJump = false;
        canMove = true;
        speed = 5;
        moveSpeed = speed;

        MAX_BP = 1000;
        BP = MAX_BP;
        A.SetActive(false);
        加速道具 = false;
    }

    private void Update()
    {
        
        if (canMove)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyIput();
            SpeedControl();
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(jumpKey))
            {
                if (BP > 0)
                {

                    BP = BP - 0.1f;
                }

            }
            else
            {
                if (BP < MAX_BP && BP > 0)
                {
                    BP = BP + 0.01f;
                }
                if (BP <= 0)
                {
                    BP = BP + 0.0001f;
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0)&&加速道具)
            {
                使用道具();
            }

            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }
    }
    private void FixedUpdate()
    {
        if (BP > 0&&canMove)
        {
            Moveplayer();
        }
    }
    private void MyIput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (canDoubleJump)
        {
            if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
            {
                //readyToJump = false;
                doublejump = true;
                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
            if (Input.GetKeyDown(jumpKey) && doublejump && !grounded)
            {

                Jump();
                doublejump = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
            {
                readyToJump = false;
                //doublejump = true;
                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }
        if((Input.GetKeyDown(slipperKey)&& Input.GetKey(KeyCode.W)&&grounded&&readyToslip)|| (Input.GetKeyDown(slipperKey) && Input.GetKey(KeyCode.UpArrow) && grounded&&readyToslip)|| (Input.GetKeyDown(slipperKey) && Input.GetKey(KeyCode.D) && grounded && readyToslip) || (Input.GetKeyDown(slipperKey) && Input.GetKey(KeyCode.RightArrow) && grounded && readyToslip) || (Input.GetKeyDown(slipperKey) && Input.GetKey(KeyCode.A) && grounded && readyToslip) || (Input.GetKeyDown(slipperKey) && Input.GetKey(KeyCode.LeftArrow) && grounded && readyToslip))
        {
            
            GameObject.Find("camaraHolder").GetComponent<position_camara>().slipping();
            moveSpeed =moveSpeed*2+4;
            readyToslip = false;
            Invoke(nameof(Resetslip), slipCooldown);
        }
        
    }
    private void Moveplayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            //moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)

            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplir, ForceMode.Force);

        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
        
    }
    private void Resetslip()
    {
        moveSpeed = (moveSpeed-4)/2;
        readyToslip = true;
        GameObject.Find("camaraHolder").GetComponent<position_camara>().reset();
    }
    public void Box()
    {
        //canDoubleJump = true;
        A.SetActive(true);
        加速道具 = true;

    }
    public void end()
    {
        canMove = false;
    }
    public void recover()
    {
        
            BP = MAX_BP;
        
    }
    public void 使用道具()
    {
        
        moveSpeed = moveSpeed*2;
        A.SetActive(false);
        Invoke(nameof(重制效果), 10);
    }
    public void 重制效果()
    {
        moveSpeed = speed;
    }
}
