using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPT : MonoBehaviour
{
    public float a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        a = Mathf.Round(PL_move.BP);
        GetComponent<Text>().text = "" + a;
    }
}
