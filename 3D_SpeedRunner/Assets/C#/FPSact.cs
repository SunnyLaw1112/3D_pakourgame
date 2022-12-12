using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSact : MonoBehaviour
{
    Animator ACT;
    // Start is called before the first frame update
    void Start()
    {
        ACT = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        
            ACT.SetBool("run", true);
        else
            ACT.SetBool("run", false);
        if (Input.GetKeyDown(KeyCode.Space))
            ACT.SetTrigger("Jump");
        
        
    }
}
