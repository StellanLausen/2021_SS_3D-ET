using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultFiedController : MonoBehaviour
{
    private Text textCompTime;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        textCompTime = GetComponent<Text>();
        textCompTime.text = gameManager.recordOne.ToString();
    }
}
