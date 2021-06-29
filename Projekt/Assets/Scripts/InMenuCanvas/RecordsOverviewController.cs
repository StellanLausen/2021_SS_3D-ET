using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InMenuCanvas
{
    public class RecordsOverviewController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        [SerializeField] private Text recordOneTextCompTime,
            recordOneTextCompPoints,
            recordOneTextCompTotal,
            recordTwoTextCompTime,
            recordTwoTextCompPoints,
            recordTwoTextCompTotal,
            recordThreeTextCompTime,
            recordThreeTextCompPoints,
            recordThreeTextCompTotal;

        private void Start()
        {
            gameManager.recordChanged.AddListener(UpdateRecords);
            UpdateRecords();
        }
        private void UpdateRecords()
        {
            recordOneTextCompTime.text = StaticMethod.TimeAsString(GameManager.RecordsLvlOne[0]);
            recordOneTextCompPoints.text = GameManager.RecordsLvlOne[1].ToString("");
            recordOneTextCompTotal.text = GameManager.RecordsLvlOne[2].ToString("");

            recordTwoTextCompTime.text = StaticMethod.TimeAsString(GameManager.RecordsLvlTwo[0]);
            recordTwoTextCompPoints.text = GameManager.RecordsLvlTwo[1].ToString("");
            recordTwoTextCompTotal.text = GameManager.RecordsLvlTwo[2].ToString("");
            
            recordThreeTextCompTime.text = StaticMethod.TimeAsString(GameManager.RecordsLvlThree[0]); 
            recordThreeTextCompPoints.text = GameManager.RecordsLvlThree[1].ToString("");
            recordThreeTextCompTotal.text = GameManager.RecordsLvlThree[2].ToString("");
        }
        public void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void LoadRecords()
        {
            gameManager.LoadRecords();
        }
    }
}
