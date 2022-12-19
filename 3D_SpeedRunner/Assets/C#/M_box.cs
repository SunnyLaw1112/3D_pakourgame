using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_box : MonoBehaviour
{
    public float distance;
    public GameObject Player, M_Box,Cam;
    // Start is called before the first frame update
    void Start()
    {
        M_Box.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Dis();
        Cam.SetActive(false);
    }
    void Dis()
    {
        distance = Vector3.Distance(Player.transform.position, M_Box.transform.position);
        if (distance < 1&&PL_move.haveTool==false)
        {
            GameObject.Find("FPScontroller").GetComponent<PL_move>().Box();

            M_Box.SetActive(false);

            Invoke(nameof(Reset), 5f);

            AudioManager.instance.Play("ItemA");
        }
    }
    private void Reset()
    {
        M_Box.SetActive(true);
    }
}
