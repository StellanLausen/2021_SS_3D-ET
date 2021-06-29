using System;
using UnityEngine;
/*
EventSystem:	Änderungen vorbehalten. Siehe in "Projekt Datei" für genaue Zeilenangaben.
Name Arbeit:	youtube Video Titel: 	How To Build An Event System in Unity
Name Autor:		youtube Channel name: 	Game Dev Guide	
URL:			https://www.youtube.com/watch?v=gx0Lt4tCDE0&t=176s	
Abrufdatum:		02.06.2021
Lizenzmodel:	ideo Lizenz:		Creative Commons-Lizenz mit Quellenangabe (Wiederverwendung erlaubt)
 */
public class EventSystem : MonoBehaviour
{
    //source EvenSystem start -source code changed
    public static EventSystem Current;
    private void Awake()
    {
        Current = this;
    }
    
    public event Action<int> ONSwitchChange;
    public void SwitchChange(int id)
    {
        if(ONSwitchChange != null)
        {
            ONSwitchChange(id);
        }
    }
    public event Action<int> ONCheckpointEnter; 
    public void CheckpointEnter(int id)
    {
        if (ONCheckpointEnter != null)
        {
            ONCheckpointEnter(id);
        }
    }
    //source EventSystem end
}
