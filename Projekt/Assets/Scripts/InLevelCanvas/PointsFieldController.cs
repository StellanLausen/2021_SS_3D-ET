using UnityEngine;
using UnityEngine.UI;

namespace InLevelCanvas
{
    public class PointsFieldController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        private Text textCompPoints;

        private void Start()
        {
            gameManager.pointsChanged.AddListener(UpdatePoints);
            
            textCompPoints = GetComponent<Text>();
            textCompPoints.text = gameManager.DynPoints.ToString();
        }
        private void UpdatePoints()
        {
            textCompPoints.text = gameManager.DynPoints.ToString();
        }
    }
}
