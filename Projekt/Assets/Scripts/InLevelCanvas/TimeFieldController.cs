using UnityEngine;
using UnityEngine.UI;

namespace InLevelCanvas
{
    public class TimeFieldController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        private Text textCompTime;
        private void Start()
        {
            textCompTime = GetComponent<Text>();
            gameManager.timeChanged.AddListener(UpdateTime);
        }
        private void UpdateTime()
        {
            textCompTime.text = StaticMethod.TimeAsString(gameManager.DynTime);
        }
    }
}