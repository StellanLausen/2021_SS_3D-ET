using UnityEngine;
using UnityEngine.UI;

namespace InMenuCanvas
{
    public class ResultFieldController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        private Text textCompTime;
        private void Start()
        {
            textCompTime = GetComponent<Text>();
            textCompTime.text = StaticMethod.TimeAsString(GameManager.GetAndResetTime());
        }
    }
}

