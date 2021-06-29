using UnityEngine;
using UnityEngine.SceneManagement;

namespace InMenuCanvas
{
    public class StartMenuController : MonoBehaviour
    {
        public void LoadLevelOverviewScene()
        {
            SceneManager.LoadScene(1);
        }
        public void LoadRecordOverviewScene()
        {
            SceneManager.LoadScene(2);
        }
        public void LoadLevelOneScene()
        {
            SceneManager.LoadScene(4);
        }
        public void ExitApplication()
        {
            Application.Quit();
        }
    }
}
