using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelOverviewController : MonoBehaviour
{
    public void LoadLevelOne()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadLevelTwo()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
