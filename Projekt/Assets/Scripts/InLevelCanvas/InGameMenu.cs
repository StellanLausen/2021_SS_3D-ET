using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InLevelCanvas
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Text textCompResult;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PauseBtnFieldController pauseBtnFieldController;
        
        [SerializeField] private GameObject pauseBtn;
        private GameObject tutorialTxt, tutorialBg;
        
        private bool isPaused = false;
        private int sceneIndex;
        private void Start()
        {
            pauseBtnFieldController.pauseChanged.AddListener(OpenInGameMenu);
            playerController.levelFinished.AddListener(OpenInGameMenuWithoutHud);
            gameManager.lostLevel.AddListener(OpenInGameMenuWithoutHud);
            
            gameObject.SetActive(false);
            sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (GameObject.Find("TutorialTxt") && GameObject.Find("TutorialBackground"))
            {
                tutorialTxt = GameObject.Find("TutorialTxt");
                tutorialBg = GameObject.Find("TutorialBackground");
            }
        }

        private void OpenInGameMenuWithoutHud()
        {
            if (tutorialBg != null)
            {
                tutorialBg.SetActive(false);
                tutorialTxt.SetActive(false);
            }
            pauseBtn.SetActive(false);
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
                textCompResult.text = StaticMethod.TimeAsString(gameManager.DynTime);
                isPaused = true;
            }  
        }
        public void TryAgainBtn()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneIndex);
        }
        public void NextBtn()
        {        
            Time.timeScale = 1;
            SceneManager.LoadScene(3);
        }
    }
}
