using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position_camara : MonoBehaviour
{
    public Transform camaraPosition;

    public float down;
    public float sensX;
    public float sensY;
    float xRotation;
    float yRotation;

    public Camera C_1;
    public Camera C_2;
    // Start is called before the first frame update
    void Start()
    {
        C_1.enabled = true;
        C_2.enabled = false;

        down = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //transform.position = camaraPosition.position;
        transform.position =new Vector3 (camaraPosition.position.x, camaraPosition.position.y-down, camaraPosition.position.z);
        if (Input.GetKeyDown(KeyCode.O))
        {
            C_1.enabled = true;
            C_2.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            C_1.enabled = false;
            C_2.enabled = true;
        }

    }
    public void slipping()
    {
        down = 0.5f;
    }
    public void reset()
    {
        down = 0;
    }
}
