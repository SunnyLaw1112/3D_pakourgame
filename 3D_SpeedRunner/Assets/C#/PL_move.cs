using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PL_move : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float speed;

    public GameObject player,A,B;

    static public float BP;
    static public float MAX_BP;

    public float groundDrag;

    static public float jumpForce;
    public float jumpCooldown;
    public float airMultiplir;

    

    bool readyToJump;
    bool canDoubleJump;
    bool canMove;
    static public bool haveTool;
    bool Atool;
    bool Btool;
    bool ToolBox;

    public static float AddScore;
    public Text score;

    public float slipCooldown;
    bool readyToslip;
    bool doublejump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode slipperKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    static public bool grounded;

    [Header("Wall Checker")]
    public static bool wallRight;
    public static bool wallLeft;
    public float wallCheckDistance;
    public float minJumpHeight;
    public RaycastHit leftWallhit;
    public RaycastHit rightWallhit;
    public LayerMask whatIsWall;

    [Header("wall up")]
    static public bool CanWallUp;
    static public bool ReadyToWallUp;
    public bool Walluping;
    public float up;
    public float saveposition_x;
    public float saveposition_z;
    public RaycastHit behindHit;
    public RaycastHit forwardWallhit;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    float ZRotation=0;
    float turnZ=0;

    static float TimeLine = 0;
    public Text Timer;

    public int whatTool;

    Vector3 moveDirection;
    Vector3 wallmove;

    Rigidbody rb;

    
    
    
    
    private void Start()
    {
        jumpForce = 5;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;
        readyToJump = true;
        readyToslip = true;
        doublejump = false;
        canDoubleJump = false;
        
        canMove = true;
        speed = 5;
        moveSpeed = speed;
        wallCheckDistance = 1.2f;
        playerHeight = 2;

        AddScore = 0;
        ReadyToWallUp = true;
        MAX_BP = 10000;
        BP = MAX_BP;
        A.SetActive(false);
        B.SetActive(false);
        haveTool = false;
        Atool = false;
        Btool = false;
    }

    private void Update()
    {
        print(jumpForce);
        CanWallUp = Physics.Raycast(transform.position, orientation.forward, out forwardWallhit, 2f)&&!Physics.Raycast(new Vector3(transform.position.x,transform.position.y+1f,transform.position.z), orientation.forward, out forwardWallhit, 3f);
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
        Walluping = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f) && !grounded;

        if (canMove)
        {
            ZRotation +=turnZ ;
            transform.rotation = Quaternion.Euler(0, 0, ZRotation);

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
            if (Input.GetKeyDown(KeyCode.Mouse0)&&haveTool)
            {
                useTool();
            }

            if (grounded)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
            timer();
            score.text = AddScore.ToString();
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKeyDown(jumpKey) && CanWallUp && ReadyToWallUp)
            {
                Invoke(nameof(Â½¶V), 0.05f);
                CanWallUp = false;

            }

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
            playerHeight = 2;
            up = 0;
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            if (ZRotation <= 0 && turnZ < 0)
            {
                turnZ = 0;
                ZRotation = 0;
                
            }
        }
        else if (!grounded )
        {
            
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplir, ForceMode.Force);
            //rb.useGravity = true;
        }
        
        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y+up, limitedVel.z);
        }
    }
    public void Jump()
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
        if (!haveTool) 
        {
            whatTool = Random.Range(1, 3);
            if (whatTool == 1)
            {
                A.SetActive(true);
                Atool = true;
            }
            if (whatTool == 2)
            {
                B.SetActive(true);
                canDoubleJump = true;
                Btool = true;
            }
            haveTool = true;
        }

    }
    public void end()
    {
        canMove = false;
    }
    public void recover()
    {
        
            BP = MAX_BP;
        
    }
    public void useTool()
    {
        if (Atool) 
        {
            AddScore = AddScore + 100;
            Atool = false;
            moveSpeed = moveSpeed * 2;
            A.SetActive(false);
            Invoke(nameof(ResetA), 3);
            haveTool = false;
        }
        if (Btool && doublejump && !grounded)
        {
            AddScore = AddScore + 100;
            Btool = false;
            B.SetActive(false);
            Jump();
            doublejump = false;
            haveTool = false;
            canDoubleJump=false;
        }
     }
    public void ResetA()
    {
        moveSpeed = speed;
    }
    public void timer()
    {
        TimeLine += Time.deltaTime;
        Timer.text = TimeLine.ToString("0.00");
    }
    public void Â½¶V()
    {
        GameObject.Find("FPScamara").GetComponent<turn_camara>().StartWallUp();
        saveposition_x = this.transform.position.x;
        saveposition_z = this.transform.position.z;
        jumpForce = 25;
        turnZ = -0.2f;
        moveSpeed = moveSpeed*2/3;
        up = 3f;
        
        Invoke(nameof(ReturnWallUp), 0.15f);

    }
    public void ReturnWallUp()
    {
        jumpForce = 5;
        up = -1.5f;
        moveSpeed = moveSpeed * 3 / 2;
        Invoke(nameof(wallUpScore), 0.15f);
    }
    public void wallUpScore()
    {
        float dis = Mathf.Pow(Mathf.Pow((saveposition_x - this.transform.position.x), 2) + Mathf.Pow((saveposition_z - this.transform.position.z), 2), 0.5f);
        if (Mathf.Abs(dis) >= 1)
        {
            AddScore = AddScore + 100;
        }
    }
    //¼o®×
    
}
