using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class keyControl : MonoBehaviour
{
    //public GameObject ShowScore;

    // Start is called before the first frame update
    static public bool End = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //ShowScore.SetActive(false);
            print("Scene restarted");
            GameObject.Find("FPScontroller").GetComponent<PL_move>().allreturn();
        }

        if (End)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //ShowScore.SetActive(false);
                print("Scene restarted");
                GameObject.Find("FPScontroller").GetComponent<PL_move>().allreturn();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Cursor.visible = true;
                SceneManager.LoadScene(1);
                GameObject.Find("FPScontroller").GetComponent<PL_move>().allreturn();
            }
        }
    }
}
