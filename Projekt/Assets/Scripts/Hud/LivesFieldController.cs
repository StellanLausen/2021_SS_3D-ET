using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LivesFieldController : MonoBehaviour
{
    private Text textCompLives;
    private GameManager gameManager;
    void Start()
    {
        gameManager  = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        textCompLives = GetComponent<Text>();
        
        gameManager.livesChanged.AddListener(UpdateLives);
        textCompLives.text = gameManager.lives.ToString();
    }

    // Update is called once per frame
    private void UpdateLives()
    {
        textCompLives.text = gameManager.lives.ToString();
    }
}
