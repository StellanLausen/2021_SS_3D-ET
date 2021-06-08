using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;

    private int _lives = 3;
    private float _dynTime = 0;
    private static float _time = 0;
    private int _points = 0;

    private static float _recordOne = 9999;
    private static float _recordTwo = 9999;
    private static float _recordThree = 9999;
    
    //Getter
    public int lives { get { return _lives; } } 
    public float dynTime { get { return _dynTime; } }
    public float time { get { return _time; } }
    public float recordOne { get { return _recordOne; } }
    public float recordTwo { get { return _recordTwo; } }
    public float recordThree { get { return _recordThree; } }
    public int points { get { return _points; } }
    
    //Events
    public UnityEvent livesChanged = new UnityEvent();
    public UnityEvent timeChanged = new UnityEvent();
    public UnityEvent pointsChanged = new UnityEvent();
    
    private string sceneName;
    void Start()
    {
        //SceneName
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        //Player only exists in a Level so NullPointer if in Menu
        if (GameObject.Find("Player"))
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.LevelFinished.AddListener(FinishedLevel);
        }

        //Timer
        StartCoroutine(TimeCall());
    }

    public void FinishedLevel()
    {
        _time = _dynTime;
        CheckRecord();
    }
    private void CheckRecord()
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
    
    //Changed GameManagerVariables
    public void RemoveLive()
    {
        _lives--;
        //Event
        livesChanged.Invoke();
    }
    public void AddLive()
    {
        if (-lives < 3)
        {
            _lives++;
        }
        livesChanged.Invoke();
    }
    public void AddPoint()
    {
        _points++;
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
        SceneManager.LoadScene(3);
    }
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(4);
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
        _dynTime += 1;
        timeChanged.Invoke();
    }
}