using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeFieldComtroller : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManagerObj;
    private GameManager gameManager;

    private Text textCompTime;
    private void Start()
    {
        gameManager  = gameManagerObj.GetComponent<GameManager>();
        textCompTime = gameObject.GetComponent<Text>();
        
        gameManager.timeChanged.AddListener(UpdateTime);
    }
    private void UpdateTime()
    {
        var time = gameManager.dynTime;
        var fancyTime = Mathf.Floor(time / 60 ).ToString("00")  + " : " + 
                          Mathf.FloorToInt(time % 60).ToString("00");
        textCompTime.text = fancyTime;
    }
}