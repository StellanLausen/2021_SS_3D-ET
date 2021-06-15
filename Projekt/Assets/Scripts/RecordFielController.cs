using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordFielController : MonoBehaviour
{
    private Text textCompLives;
    private GameManager gameManager;
    void Start()
    {
        gameManager  = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        textCompLives = GetComponent<Text>();
        
        textCompLives.text = gameManager.Time.ToString();
    }
}