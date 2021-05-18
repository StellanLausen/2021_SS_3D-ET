using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class HudController : MonoBehaviour
{
    private Boolean isPaused = false;
    private Text textCompTime;

    public GameObject InGameMenu;
    public GameObject ResultChar;
    private GameManager gameManager;
    
    public UnityEvent recordChanged = new UnityEvent();
    void Start()
    {
        InGameMenu.SetActive(false);
        textCompTime = ResultChar.GetComponent<Text>();

        //textCompTime = GameObject.Find("ResultChar").GetComponent<Text>();
        gameManager  = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void PauseBtn()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            InGameMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            textCompTime.text = gameManager.time.ToString();
            InGameMenu.SetActive(true);
        }
    }
    public void TryAgainBtn()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void NextBtn()
    {        
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void FinishedHud()
    {
        isPaused = false;
        PauseBtn();
        recordChanged.Invoke();
    }
}
