using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeFieldComtroller : MonoBehaviour
{
    private Text textCompTime;
    private GameManager gameManager;
    void Start()
    {
        gameManager  = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        textCompTime = GetComponent<Text>();
        
        gameManager.timeChanged.AddListener(UpdateTime);
        textCompTime.text = gameManager.time.ToString();
    }

    // Update is called once per frame
    void UpdateTime()
    {
        textCompTime.text = gameManager.time.ToString();
    }
}