using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissonTrigger : MonoBehaviour
{
    [SerializeField] private Image missionBackground;
    [SerializeField] private Text missonText;
    [TextArea(3, 6)]
    [SerializeField] private string missionContent;
    //public string missionContent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            missionBackground.enabled = true;
            missonText.enabled = true;
            missonText.text = missionContent;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            missionBackground.enabled = false;
            missonText.enabled = false;

        }
    }
    void Start()
    {
        missionBackground.enabled = false;
        missonText.enabled = false;

    }


}