using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private Text textCompTutorial;
    private void Start()
    {
        EventSystem.Current.ONCheckpointEnter += ChangeTutorialText;
        textCompTutorial.text = "Steuere die Kugel mit hilfe von den WASD-Tasten zum nächsten Checkpoint.\n\nEinen Checkpoint kannst du anhand einer kleinen Fahne erkennen.";
    }

    private void ChangeTutorialText(int id)
    {
        switch (id)
        {
            case 1 :
                textCompTutorial.text = "Mit der Leertaste können Sie Springen, probieren Sie es aus!";
                break;
            case 2 : 
                textCompTutorial.text = "Wenn Sie die Leertaste kurzzeitig gedrückt halten, könne Sie höher Springen.";
                break;
            case 3 :
                textCompTutorial.text = "Mit der 'C' Taste können Sie die Kamera zwischen  beweglich und unbeweglich umschalten.\n\nUm die Kamera zu bewegen halten Sie die linke Maustaste und bewegen Sie die Maus.";
                break;
            case 4 :
                textCompTutorial.text = "Sie können durch Berührung Coins einsammeln.";
                break;
            case 5 :
                textCompTutorial.text = "Mit Trampolinen können sie besonders hoch springen!";
                break;
            case 6 :
                textCompTutorial.text = "Vill beinhaltet der Block mehr als man denkt.. finde es jetzt heraus!";
                break;
            case 7 :
                textCompTutorial.text = "Zum Beenden einese Levels springen Sie in das Portal";
                break;
        }
    }
    
}
