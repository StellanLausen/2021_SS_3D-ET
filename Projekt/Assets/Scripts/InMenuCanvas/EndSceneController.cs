using UnityEngine;
using UnityEngine.SceneManagement;

namespace InMenuCanvas
{
    public class EndSceneController : MonoBehaviour
    {
        public void ExitToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
