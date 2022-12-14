using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hoverOverManager : MonoBehaviour
{
    public static hoverOverManager _instance;

    public TextMeshProUGUI textComponent;

    private void Awake() 
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void SetAndShowHoverOver(string message) 
    {
        gameObject.SetActive(true);
        textComponent.text = message;
    }

    public void HideHoverOver() 
    {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
    }

}
