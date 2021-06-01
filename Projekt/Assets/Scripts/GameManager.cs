using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private HudController hudController;
    
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
        
        //EventListener
        if (GameObject.Find("HudController"))
        {
            hudController = GameObject.Find("HudController").GetComponent<HudController>(); 
            hudController.win.AddListener(finishedLevel);   
        }
        
        //Timer
        StartCoroutine(timeCall());
    }

    public void finishedLevel()
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

    public void AddPoint()
    {
        _points++;
        //Event
        pointsChanged.Invoke();
    }

    //LevelScene
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(1);
    }
    
    //StartScene
    public void ExitApplication()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    //Scenes
    public void LoadLevelScene()
    {
        SceneManager.LoadScene(3);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    
    //Timer
    IEnumerator timeCall()
    {
        while (true)
        {
            timeCount();
            yield return new WaitForSeconds(1);
        }
    }
    void timeCount()
    {
        _dynTime += 1;
        timeChanged.Invoke();
    }
}