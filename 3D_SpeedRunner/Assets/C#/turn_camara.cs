using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn_camara : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    public float zRotation=0;
    float addX = 0;
    float addZ = 0;
    public bool readyWallJump;

    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
        readyWallJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX + addX;
        
        xRotation -= mouseY;
        zRotation += addZ;
        if (zRotation <= 0&&addZ<0)
        {
            addZ = 0;
            zRotation = 0;
            PL_move.ReadyToWallUp = true;
        }
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        wallJump();
        
        resetWhenWallJump();
        
        
    }
    void wallJump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) && (PL_move.wallRight == true) && !PL_move.grounded))
        {
            
            if (readyWallJump)
            {
                PL_move.BP = PL_move.BP - 50;
                PL_move.MoveScore = PL_move.MoveScore + 100;
                PL_move.jumpForce = PL_move.OjumpForce*6/5;
                GameObject.Find("FPScontroller").GetComponent<PL_move>().Jump();
                readyWallJump = false;
            }
            


        }
        else if (readyWallJump && (Input.GetKeyDown(KeyCode.Space) && (PL_move.wallLeft == true && !PL_move.grounded)))
        {
            PL_move.BP = PL_move.BP - 50;
            PL_move.MoveScore = PL_move.MoveScore + 150;
            PL_move.jumpForce = PL_move.OjumpForce * 6 / 5;
            GameObject.Find("FPScontroller").GetComponent<PL_move>().Jump();
            readyWallJump = false;
            

        }
        else if(PL_move.grounded)
        {
            addX = 0;
            PL_move.jumpForce = PL_move.OjumpForce*3/2;
        }
    }
    void resetWhenWallJump()
    {
        if (PL_move.wallRight == false && PL_move.wallLeft == false || PL_move.grounded)
        {
            readyWallJump = true;
        }
    }
    public void StartWallUp()
    {
        
            addZ = 0.05f;
        PL_move.ReadyToWallUp = false;
        Invoke(nameof(endWallUp), 0.3f);
        

    }
    public void endWallUp()
    {
        addZ = -0.05f;

    }

}
