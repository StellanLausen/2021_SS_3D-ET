using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Hud
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private Text textCompResult;
        [SerializeField] private PlayerController playerController;
        
        private bool isPaused = false;
        private int sceneIndex;
        private void Start()
        {
            playerController.levelFinished.AddListener(Finished);
            gameObject.SetActive(false);
            
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
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
            SceneManager.LoadScene(sceneIndex);
        }
        public void NextBtn()
        {        
            Time.timeScale = 1;
            SceneManager.LoadScene(2);
        }
    }
}
