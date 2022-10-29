using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position_camara : MonoBehaviour
{
    public Transform camaraPosition;

    public float down;

    public Camera C_1;
    // Start is called before the first frame update
    void Start()
    {
        C_1.enabled = true;
       
        down = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = camaraPosition.position;
        transform.position =new Vector3 (camaraPosition.position.x, camaraPosition.position.y-down, camaraPosition.position.z);
        
        
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
