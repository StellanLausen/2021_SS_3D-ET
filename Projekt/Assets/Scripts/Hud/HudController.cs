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

    private GameManager gameManager;
    private PlayerController playerController;
    
    //private GameObject inGameMenu;
    private GameObject resultChar;
    private GameObject result;
    private GameObject tryAgain;
    private GameObject next;
    private GameObject pauseBtn;

    void Awake()
    {
        //inGameMenu = GameObject.Find("InGameMenu");
        resultChar = GameObject.Find("ResultChar");

        result = GameObject.Find("Result");
        tryAgain = GameObject.Find("TryAgainBtn");
        next = GameObject.Find("NextBtn");
        pauseBtn = GameObject.Find("PauseBtn");
        
        gameManager  = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Start()
    {
        //Debug.Log("Start " +inGameMenu);
        //Debug.Log("Start " +gameManager);
        
        result.SetActive(false);
        tryAgain.SetActive(false);
        next.SetActive(false);
        //inGameMenu.SetActive(false);
        
        textCompTime = resultChar.GetComponent<Text>();
        playerController.LevelFinished.AddListener(Finished);
    }
    public void OpenInGameMenu()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            //inGameMenu.SetActive(false);
            
            result.SetActive(false);
            tryAgain.SetActive(false);
            next.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            //Debug.Log(gameManager);
            textCompTime.text = gameManager.dynTime.ToString();
            //Debug.Log("OpenMenu2 " + inGameMenu);
            //inGameMenu.SetActive(true);
            
            result.SetActive(true);
            tryAgain.SetActive(true);
            next.SetActive(true);
        }
    }
    public void Finished()
    {
        pauseBtn.SetActive(false);
        OpenInGameMenu();
    }
    private void TryAgainBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    private void NextBtn()
    {        
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}
