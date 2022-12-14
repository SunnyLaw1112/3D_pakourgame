using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finish_one : MonoBehaviour
{
    public float distance;
    public GameObject Player, End,Bar;
    public Text Ending;
    public GameObject ShowScore;

    // Start is called before the first frame update
    void Start()
    {
        Ending.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //Dis();
    }

    //void Dis()
    //{
    //    distance = Vector3.Distance(Player.transform.position, End.transform.position);
    //    if (distance < 3)
    //    {
    //        GameObject.Find("FPScontroller").GetComponent<PL_move>().end();
    //        print("GOOD");
    //        Bar.SetActive(false);
    //        Ending.text = "恭喜你到達!";
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.Find("FPScontroller").GetComponent<PL_move>().end();
            print("GOOD");
            Bar.SetActive(false);
            Ending.text = "恭喜你到達!";
        }
    }
}
