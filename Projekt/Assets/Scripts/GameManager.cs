using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;

    private int lives = 3;
    private int points = 0;
    private float dynTime = 0;
    private static float _time = 0;

    private static float _recordOne;
    private static float _recordTwo = 9999;
    private static float _recordThree = 9999;
    
    //Getter
    public int Lives => lives;
    public int Points => points;
    public float DynTime => dynTime;
    public float Time => _time;
    public static float RecordOne => _recordOne;
    public float recordTwo => _recordTwo;
    public float recordThree => _recordThree;

    //Events
    public UnityEvent livesChanged = new UnityEvent();
    public UnityEvent timeChanged = new UnityEvent();
    public UnityEvent pointsChanged = new UnityEvent();
    
    private string sceneName;
    void Start()
    {
        //SceneName
        sceneName = SceneManager.GetActiveScene().name;

        //Player only exists in a Level so NullPointer if in Menu
        if (GameObject.Find("Player"))
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.levelFinished.AddListener(FinishedLevel);
        }

        //Timer
        StartCoroutine(TimeCall());
    }
    private void FinishedLevel()
    {
        _time = dynTime;
        CheckRecord();
    }
    private static void CheckRecord()
    {
        if (_time < _recordOne)
        {
            _recordOne = _time;
        }else if (_time < _recordTwo)
        {
            _recordTwo = _time;
        }else if (_time < _recordThree)
        {
            _recordThree = _time;
        }
    }
    
    //Changed GameManagerVariables UnityEvents
    public void RemoveLive()
    {
        lives--;
        //Event
        livesChanged.Invoke();
    }
    public void AddLive()
    {
        if (-Lives < 3)
        {
            lives++;
        }
        livesChanged.Invoke();
    }
    public void AddPoint()
    {
        points++;
        //Event
        pointsChanged.Invoke();
    }
    
    //LoadScenes
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadLevelScene()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(4);
    }
    public void LoadLevelThree()
    {
        SceneManager.LoadScene(5);
    }
    
    //Game Functions
    public void ExitApplication()
    {
        Application.Quit();
    }
    
    //Timer
    private IEnumerator TimeCall()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            TimeCount();
        }
    }
    private void TimeCount()
    {
        dynTime += 1;
        timeChanged.Invoke();
    }
}