using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PL_move : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float speed;

    public GameObject player, A, B, C,ShowScore;

    

    static public float BP;
    static public float MAX_BP;

    public float groundDrag;

    static public float jumpForce;
    static public float OjumpForce;
    public float jumpCooldown;
    public float airMultiplir;

    [Header("grapple")]
    public static float grapplingSpeed;
    public static bool grapple;
    public GameObject UI_O;
    

    bool readyToJump;
    bool canDoubleJump;
    bool canMove;

    [Header("Tool")]
    static public bool haveTool;
    public bool Atool;
    public bool Btool;
    public bool Ctool;
    
    bool ToolBox;

    [Header("Score")]
    public static float AddScore;
    public Text score;
    static public float TimeScore;
    static public float ToolScore;
    static public float MoveScore;

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

    [Header("Orientation")]
    public Transform orientation;
    

    float horizontalInput;
    float verticalInput;
    float ZRotation=0;
    float turnZ=0;

    [Header("UI")]
    static float TimeLine = 0;
    public Text Timer;
    public GameObject UI_score, UI_time, UI_tool;
    public bool End;

    public int whatTool;

    Vector3 moveDirection;
    Vector3 grapplingDirection;
    Vector3 wallmove;

    Rigidbody rb;

    [Header("Climb")]
    public float Height = 0;
    public bool Climbing = false;


    private void Start()
    {
        AudioManager.instance.Stop("BGM2");
        AudioManager.instance.Play("BGM1");

        OjumpForce = 9f;
        jumpForce = OjumpForce;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;
        readyToJump = true;
        readyToslip = true;
        doublejump = false;
        canDoubleJump = false;
        grapple = false;
        
        canMove = true;
        speed = 7;
        moveSpeed = speed;
        wallCheckDistance = 1.2f;
        playerHeight = 2;
        grapplingSpeed = 0;

        AddScore = 0;
        ToolScore = 0;
        TimeScore = 0;
        MoveScore = 0;

        ReadyToWallUp = true;

        MAX_BP = 10000;
        BP = MAX_BP;

        A.SetActive(false);
        B.SetActive(false);
        C.SetActive(false);
        UI_score.SetActive(true);
        UI_time.SetActive(true);
        UI_tool.SetActive(true);
        UI_O.SetActive(false);
        

        haveTool = false;
        Atool = false;
        Btool = false;
        Ctool = false;
    }


private void Update()
    {
        if (canMove)
        {


            //print(jumpForce);
            Cursor.visible = false;
            CanWallUp = Physics.Raycast(transform.position, orientation.forward, out forwardWallhit, 2f) && !Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), orientation.forward, out forwardWallhit, 3f);
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.6f + 0.2f, whatIsGround);
            wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance);
            wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance);
            Walluping = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f) && !grounded;
            AddScore = ToolScore + MoveScore;
            ShowScore.SetActive(false);
            if (this.transform.position.y <= 100)
            {
                MoveScore = MoveScore - 500;
                
                transform.position = new Vector3(208.9f, 152f, -34.85f);
            }
            timer();

            if (!Climbing)
            {
                Physics.gravity = new Vector3(0, -20F, 0);
                ZRotation += turnZ;
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
                if (Input.GetKeyDown(KeyCode.Mouse0) && haveTool)
                {
                    useTool();
                }

                if (grounded)
                    rb.drag = groundDrag;
                else
                    rb.drag = 0;
                
                score.text = AddScore.ToString();
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                if (Input.GetKeyDown(jumpKey) && CanWallUp && ReadyToWallUp)
                {
                    //Invoke(nameof(wallup), 0.05f);
                    CanWallUp = false;

                }

            }
        }

    }
    private void FixedUpdate()
    {
        if (BP > 0&&canMove)
        {
            
                Moveplayer();
            if (grapple)
                grappling();
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
            if (!Climbing)
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplir, ForceMode.Force);
                Physics.gravity = new Vector3(0, -20F, 0);
                if (up >= -0.1f)
                    up = up - 0.01f;
                //rb.useGravity = true;
            }
        }
        
        
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x , 0f, rb.velocity.z);

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
        //MoveScore = MoveScore + 100;
    }
    public void Box()
    {
        //canDoubleJump = true;
        if (!haveTool) 
        {
            whatTool = Random.Range(1, 4);
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
            if (whatTool == 3)
            {
                C.SetActive(true);
                UI_O.SetActive(true);
                Ctool = true;
            }
            haveTool = true;
        }

    }
    public void end()
    {
        canMove = false;
            UI_score.SetActive(false);
        UI_time.SetActive(false);
        UI_tool.SetActive(false);
        AllScore.End = true;
        keyControl.End = true;
        if (TimeLine <= 75)
        {
            TimeScore = 1000;
        }
        if (TimeLine > 75&&TimeLine<=150)
        {
            TimeScore = 500;
        }
        if (TimeLine > 150)
        {
            TimeScore = 250;
        }
    }
    public void recover()
    {
        
            BP = MAX_BP;
        
    }
    public void useTool()
    {
        if (Atool) 
        {
            ToolScore = ToolScore + 150;
            Atool = false;
            moveSpeed = moveSpeed * 2;
            A.SetActive(false);
            Invoke(nameof(ResetA), 1);
            haveTool = false;
            AudioManager.instance.Play("SpeedUp");
        }
        if (Btool && doublejump && !grounded)
        {
            ToolScore = ToolScore + 150;
            Btool = false;
            B.SetActive(false);
            Jump();
            doublejump = false;
            haveTool = false;
            canDoubleJump=false;
            AudioManager.instance.Play("2jump");
        }
        if (Ctool)
        {
            GameObject.Find("grapplingGun").GetComponent<zhuaZi>().StarGrapple();
            AudioManager.instance.Play("Hook");
        }
        
     }
    public void ResetA()
    {
        moveSpeed = speed;
    }
    public void timer()
    {
        if (canMove)
        {
            TimeLine += Time.deltaTime;
            Timer.text = TimeLine.ToString("0.00");
        }

    }
    public void wallup()
    {
        GameObject.Find("FPScamara").GetComponent<turn_camara>().StartWallUp();
        saveposition_x = this.transform.position.x;
        saveposition_z = this.transform.position.z;
        jumpForce = OjumpForce*3;
        turnZ = -0.2f;
        moveSpeed = moveSpeed*2/3;
        up = 3f;
        
        Invoke(nameof(ReturnWallUp), 0.15f);

    }
    public void ReturnWallUp()
    {
        jumpForce = OjumpForce;
        up = -1.5f;
        moveSpeed = moveSpeed * 3 / 2;
        Invoke(nameof(wallUpScore), 0.15f);
    }
    public void wallUpScore()
    {
        float dis = Mathf.Pow(Mathf.Pow((saveposition_x - this.transform.position.x), 2) + Mathf.Pow((saveposition_z - this.transform.position.z), 2), 0.5f);
        if (Mathf.Abs(dis) >= 1)
        {
            MoveScore = MoveScore + 100;
        }
    }
    public void grappling()
    {
        grapplingSpeed += 0.3f;
        rb.AddForce((orientation.forward+ Vector3.up*0.03f) * moveSpeed * grapplingSpeed, ForceMode.Impulse);
        
    }
    public void EndGrapple()
    {
        
        Ctool = false;
        C.SetActive(false);
        UI_O.SetActive(false);
        haveTool = false;
    }
    //¼o®×
    void OnCollisionStay(Collision Latter)
    {
        if(Latter.gameObject.tag == "Climb")
        {

            if(Input.GetKey(KeyCode.W)&!grounded)
            {
            Climbing = true;
                Physics.gravity = new Vector3(0, 0, 0);
                rb.AddForce(moveDirection.normalized * moveSpeed * 0 * airMultiplir, ForceMode.Force);
            }
            
            if (Climbing == true && Input.GetKey(KeyCode.Space))
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(0, rb.velocity.y + 0.1f, 0);
        }
        else if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(0, rb.velocity.y - 0.1f, 0);
            }
            else
            rb.velocity = new Vector3(0, 0, 0);

            if (grounded)
            {
                Climbing = false;
            }
        }
        
        

    }
    private void OnCollisionExit(Collision Latter)
    {
        Climbing = false;
        
    }
    public void allreturn()
    {
        //AddScore = 0;
        //MoveScore = 0;
        //ToolScore = 0;
        transform.position = new Vector3(208.9f, 152f, -34.85f);
        TimeLine = 0;
        AllScore.End = false;
        keyControl.End = false;
        canMove = true;
    }

}
