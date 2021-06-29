using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace InLevelCanvas
{
    public class PauseBtnFieldController : MonoBehaviour
    {
        public UnityEvent pauseChanged = new UnityEvent();
        [SerializeField] private GameObject pauseSymbol, playSymbol;
        private bool isPaused = false;
        
        private void Update()
        {            
            pauseSymbol.SetActive(!isPaused);
            playSymbol.SetActive(isPaused);
            
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                isPaused = !isPaused;
                pauseChanged.Invoke();
            }
        }
        public void PauseGameBtn()
        {
            isPaused = !isPaused;
            pauseChanged.Invoke();
        }
    }
}
