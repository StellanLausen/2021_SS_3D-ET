using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject resultChar;
    private Text textCompResult;

    [SerializeField] 
    private GameObject player;
    private PlayerController playerController;

    [SerializeField] 
    private GameObject gameManagerObj;
    private GameManager gameManager;
    
    private bool isPaused = false;
    void Start()
    {
        textCompResult = resultChar.GetComponent<Text>();
        gameManager = gameManagerObj.GetComponent<GameManager>();
        playerController = player.GetComponent<PlayerController>();

        playerController.LevelFinished.AddListener(Finished);
        gameObject.SetActive(false);
    }
    private void Finished()
    {
        OpenInGameMenu();
    }
    public void OpenInGameMenu()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);

            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
            textCompResult.text =
                GameObject.Find("FancyFunctionDELETE!!!LATER!!!").GetComponent<fancyTime>().FancyTime();

            isPaused = true;
        }  
    }
    public void TryAgainBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void NextBtn()
    {        
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}
