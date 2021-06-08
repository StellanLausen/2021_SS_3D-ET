using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fancyTime : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    public string FancyTime()
    {
        var time = gameManager.dynTime;
        var fancyTime = Mathf.Floor(time / 60 ).ToString("00")  + " : " + 
                        Mathf.FloorToInt(time % 60).ToString("00");
       return fancyTime;
    }
}
