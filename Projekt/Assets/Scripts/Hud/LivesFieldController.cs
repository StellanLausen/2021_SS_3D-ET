using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LivesFieldController : MonoBehaviour
{
    private Text textCompLives;
    private GameManager gameManager;

    private GameObject lifeBarOne;
    private GameObject lifeBarTwo;
    private GameObject lifeBarThree;

    void Awake()
    {
        gameManager  = GameObject.Find("GameManager").GetComponent<GameManager>();
        lifeBarOne = GameObject.Find("LifeBarOne");
        lifeBarTwo = GameObject.Find("LifeBarTwo");
        lifeBarThree = GameObject.Find("LifeBarThree");
    }
    void Start()
    {
        gameManager.livesChanged.AddListener(UpdateLives);
    }

    private void UpdateLives()
    {
        switch (gameManager.Lives)
        {
            case 2 :
                lifeBarThree.SetActive(false);
                break;
            case 1 :
                lifeBarTwo.SetActive(false);
                break;
            case 0 :
                lifeBarOne.SetActive(false);
                break;
        }
        
    }
}
