using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_box : MonoBehaviour
{
    public float distance;
    public GameObject Player, M_Box;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ¶ZÂ÷();
    }
    void ¶ZÂ÷()
    {
        distance = Vector3.Distance(Player.transform.position, M_Box.transform.position);
        if (distance < 1)
        {
            GameObject.Find("FPScontroller").GetComponent<PL_move>().Box();

            Destroy(M_Box);


        }
    }
}
