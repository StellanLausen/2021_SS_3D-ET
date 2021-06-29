using UnityEngine;
using UnityEngine.SceneManagement;

namespace InMenuCanvas
{
    public class LevelOverviewController : MonoBehaviour
    {
        public void LoadLevelOne()
        {
            SceneManager.LoadScene(4);
        }
        public void LoadLevelTwo()
        {
            SceneManager.LoadScene(5);
        }
        public void LoadLevelThree()
        {
            SceneManager.LoadScene(6);
        }
        public void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
