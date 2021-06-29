using UnityEngine;
using UnityEngine.UI;

namespace InLevelCanvas
{
    public class TutorialFieldController : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Text textCompTutorial;
    
        string CheckPoint0txt = "Steuere die Kugel mit Hilfe von den WASD-Tasten zum nächsten Checkpoint.\n\nEinen Checkpoint kannst du anhand einer kleinen Fahne erkennen.";
        string CheckPoint1txt = "Mit der Leertaste kannst du Springen, probier es aus!";
        string CheckPoint2txt = "Wenn du die Leertaste kurzzeitig gedrückt hälst, kannst du höher Springen.";
        string CheckPoint3txt = "Mit den tasten X, C, V, kannst du zwischen verschiedenen Kameras umschalten.\n Vor dir Befindet sich ein Ölfleck auf dem du nicht springen kannst.";
        string CheckPoint4txt = "Du kannst durch Berührung Coins einsammeln.";
        string CheckPoint5txt = "Mit Trampolinen kannst du besonders hoch springen!";
        string CheckPoint6txt = "Vielleicht beinhaltet der Block mehr als man denkt.. finde es jetzt heraus!";
        string CheckPoint7txt = "Wenn du in den kleinen Kreis springs hast du gewonnen.";
    
        private void Start()
        {
            playerController.resetMap.AddListener(ResetMap);
            EventSystem.Current.ONCheckpointEnter += ChangeTutorialText;
            textCompTutorial.text = CheckPoint0txt;
        }

        private void ChangeTutorialText(int id)
        {
            switch (id)
            {
                case 1 :
                    textCompTutorial.text = CheckPoint1txt;
                    break;
                case 2 : 
                    textCompTutorial.text = CheckPoint2txt;
                    break;
                case 3 :
                    textCompTutorial.text = CheckPoint3txt;
                    break;
                case 4 :
                    textCompTutorial.text = CheckPoint4txt;
                    break;
                case 5 :
                    textCompTutorial.text = CheckPoint5txt;
                    break;
                case 6 :
                    textCompTutorial.text = CheckPoint6txt;
                    break;
                case 7 :
                    textCompTutorial.text = CheckPoint7txt;
                    break;
            }
        }

        private void ResetMap()
        {
            textCompTutorial.text = CheckPoint0txt;
        }
    
    }
}
