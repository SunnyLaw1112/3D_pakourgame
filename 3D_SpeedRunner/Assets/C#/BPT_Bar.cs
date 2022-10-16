using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPT_Bar : MonoBehaviour
{

    public float bar_x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(PL_move.BP);
        bar_x = 3f*PL_move.BP/PL_move.MAX_BP;
        transform.localScale = new Vector3(bar_x,0.1f,1f);
    }
}
