using UnityEngine;

namespace InLevelCanvas
{
    public class LivesFieldController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameObject lifeBarOne,
            lifeBarTwo, 
            lifeBarThree;
        
        private void Start()
        {
            gameManager.livesChanged.AddListener(UpdateLives);
        }
        private void UpdateLives()
        {
            switch (gameManager.Lives)
            {
                case 3 :
                    lifeBarThree.SetActive(true);
                    lifeBarTwo.SetActive(true);
                    lifeBarOne.SetActive(true);
                    break;
                case 2 :
                    lifeBarThree.SetActive(false);
                    lifeBarTwo.SetActive(true);
                    lifeBarOne.SetActive(true);
                    break;
                case 1 :
                    lifeBarThree.SetActive(false);
                    lifeBarTwo.SetActive(false);
                    lifeBarOne.SetActive(true);
                    break;
                case 0 :
                    lifeBarThree.SetActive(false);
                    lifeBarTwo.SetActive(false);
                    lifeBarOne.SetActive(false);
                    break;
            }
        
        }
    }
}
