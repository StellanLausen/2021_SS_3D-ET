using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;

    private int lives = 3, dynPoints = 0;
    private static int _points = 0;
    private static float _time = 0;
    private float dynTime = 0;
    private string sceneName;

    private static float[] _recordsLvlOne = {0f,0f,0f},  
        _recordsLvlTwo = {0f,0f,0f},
        _recordsLvlThree = {0f,0f,0f};

    //Getter
    public int Lives => lives;
    public int DynPoints => dynPoints;
    public float DynTime => dynTime;
    public static float Time => _time;
    public static float GetAndResetTime()
    {
        var returnTime = _time;
        _time = 0;
        return returnTime;
    }
    public static float[] RecordsLvlOne => _recordsLvlOne;
    public static float[] RecordsLvlTwo => _recordsLvlTwo;
    public static float[] RecordsLvlThree => _recordsLvlThree;

    //Events
    public UnityEvent livesChanged = new UnityEvent();
    public UnityEvent pointsChanged = new UnityEvent();
    public UnityEvent timeChanged = new UnityEvent();
    public UnityEvent recordChanged = new UnityEvent();
    public UnityEvent lostLevel = new UnityEvent();

    private void Start()
    {
        //SceneName
        sceneName = SceneManager.GetActiveScene().name;

        //Check if in Level
        if (GameObject.Find("Player"))
        {
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            playerController.levelFinished.AddListener(FinishedLevel);
        
            //Timer
            StartCoroutine(TimeCall());
        }
    }
    public void FinishedLevel()
    {
        _points = dynPoints;
        _time = dynTime;
        ChangeRecords();
    }

    //Save&Load Records
    private void ChangeRecords()
    {
        var total = _time - _points * 0.5f;
        
        switch (sceneName)
        {
            case "LevelOne" :
                if (total < _recordsLvlOne[2] || _recordsLvlOne[0] == 0)
                {
                    _recordsLvlOne[0] = _time;
                    _recordsLvlOne[1] = _points;
                    _recordsLvlOne[2] = total;
                }
                break;
            case "LevelTwo" :
                if (total < _recordsLvlTwo[2] || _recordsLvlTwo[0] == 0)
                {
                    _recordsLvlTwo[0] = _time;
                    _recordsLvlTwo[1] = _points;
                    _recordsLvlOne[2] = total;
                }
                break;
            case "LevelThree" :
                if (total < _recordsLvlThree[2] || _recordsLvlThree[0] == 0)
                {
                    _recordsLvlThree[0] = _time;
                    _recordsLvlThree[1] = _points;
                    _recordsLvlOne[2] = total;
                }
                break;
        }
    }
    private void SaveRecords()
    {
        SaveSystem.SaveRecords(this);
    }
    public void LoadRecords()
    {
        var data = SaveSystem.LoadRecords();
        _recordsLvlOne = data.recordsLvlOne;
        _recordsLvlTwo = data.recordsLvlTwo;
        _recordsLvlThree = data.recordsLvlThree;
        recordChanged.Invoke();
    }
    
    //Changed GameManagerVariables -UnityEvents
    public void RemoveLive()
    {
        lives--;
        if (lives <= 0)
        {
            lostLevel.Invoke();
        }
        //Event
        livesChanged.Invoke();
    }
    public void AddLive()
    {
        if (lives < 3)
        {
            lives++;
        }
        livesChanged.Invoke();
    }
    public void AddPoint()
    {
        dynPoints++;
        //Event
        pointsChanged.Invoke();
    }
    
    //Game Functions
    public void OnApplicationQuit()
    {
        SaveRecords();
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