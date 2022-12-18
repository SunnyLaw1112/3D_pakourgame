using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllScore : MonoBehaviour
{
    public Text showTime;
    public Text showABC, showMove, showItem, showScore;
    public GameObject ShowScore;
    public float TimeLine;
    public float Timer = 0f;
    public float finalScore = 0f;
    public float minute = 0;
    public float TimeScore = 0, MoveScore = 0, ItemScore = 0, ScoreScore = 0;
    static public bool End=false;

    // Start is called before the first frame update
    void Start()
    {
        ShowScore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        finalScore = PL_move.TimeScore + PL_move.ToolScore + PL_move.MoveScore;
        if (End)
            ScoreCount(); 
        else if(!End)
            ShowScore.SetActive(false);
    }

    public void ScoreCount()
    {
        ShowScore.SetActive(true);
        if (MoveScore < PL_move.MoveScore)
        {
            MoveScore = MoveScore + 5;
            showMove.text = MoveScore.ToString();
        }
        if (ItemScore < PL_move.ToolScore)
        {
            ItemScore = ItemScore + 5;
            showItem.text = ItemScore.ToString();
        }
        if (TimeScore < PL_move.TimeScore)
        {
            TimeScore = TimeScore + 5;
            showTime.text = TimeScore.ToString();
        }
        if (ScoreScore < finalScore)
        {
            ScoreScore = ScoreScore + 5;
            showScore.text = ScoreScore.ToString();
        }
        else
        {
            if (finalScore <= 1000)
            {
                showABC.text = "C";
            }
            else if (finalScore > 1000 && finalScore <= 1500)
            {
                showABC.text = "B";
            }
            else if (finalScore > 1500 && finalScore <= 2000)
            {
                showABC.text = "A";
            }
            else if (finalScore > 2000 && finalScore <= 2500)
            {
                showABC.text = "S";
            }
        }
    }
}
