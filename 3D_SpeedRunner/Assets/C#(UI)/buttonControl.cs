using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play() 
    {
        SceneManager.LoadScene(1);
    }

    public void EnterLevel1() 
    {
        SceneManager.LoadScene(3);
        Debug.Log("Enter Level 1");
    }

    public void SavingScenes() 
    {
        Debug.Log("Enter saving scenes");
        SceneManager.LoadScene(2);
    }



}
