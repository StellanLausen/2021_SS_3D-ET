using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudFieldController : MonoBehaviour
{
    public Text textCompTime;

    private GameManager gameManager;
    private GameObject TimeChar;
    void Start()
    {
        gameManager  = GameObject.Find("GameManager").GetComponent<GameManager>();
        TimeChar = GameObject.Find("TimeChar");
        
        textCompTime = TimeChar.GetComponent<Text>();
        
        gameManager.timeChanged.AddListener(UpdateTime);

        void UpdateTime()
        {
            float totalTime = gameManager.dynTime;
            string realTime = Mathf.Floor(totalTime / 60 ).ToString("00")  + " : " + 
                              Mathf.FloorToInt(totalTime % 60).ToString("00");
            textCompTime.text = realTime;
        }
    }
}
