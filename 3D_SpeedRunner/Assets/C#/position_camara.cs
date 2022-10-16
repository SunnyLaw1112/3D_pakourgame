using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position_camara : MonoBehaviour
{
    public Transform camaraPosition;

    public float down;

    public Camera C_1, C_2;
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
