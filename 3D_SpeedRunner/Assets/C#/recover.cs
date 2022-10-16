using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recover : MonoBehaviour
{
    public float distance;
    public GameObject Player, Rebox;
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
        distance = Vector3.Distance(Player.transform.position, Rebox.transform.position);
        if (distance < 1)
        {
            GameObject.Find("FPScontroller").GetComponent<PL_move>().recover();

            Destroy(Rebox);


        }
    }
}
