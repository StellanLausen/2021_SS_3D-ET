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
    private static float _time = 0;

    private static float _recordOne = 9999;
    private float _recordTwo;
    private float _recordThree;
    
    //Getter
    public int lives { get { return _lives; } } 
    public float time { get { return _time; } }
    public float recordOne { get { return _recordOne; } }
    //Events
    public UnityEvent livesChanged = new UnityEvent();
    public UnityEvent timeChanged = new UnityEvent();

    private string sceneName;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        //EventListener
        if (GameObject.Find("HudController"))
        {
            hudController = GameObject.Find("HudController").GetComponent<HudController>(); 
            hudController.recordChanged.AddListener(CheckRecord);   
        }
    }

    private void CheckRecord()
    {
        if (_time < _recordOne)
        {
            _recordOne = _time;
        }
    }

    //Changed GameManagerVariables
    public void RemoveLive()
    {
        _lives--;
        //Event
        livesChanged.Invoke();
    }
    public void AddTime(float setTime)
    {
        _time = setTime;
        timeChanged.Invoke();
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

    
    //Timer with Update
    float timer = 0.0f;
    void Update()
    {
        if (sceneName == "SampleScene")
        {
            timer += Time.deltaTime;
            float seconds = timer % 60;
            AddTime(seconds);   
        }
    }
}